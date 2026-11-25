# Stage 1: Build the .NET Framework 4.5 ASP.NET MVC 5 solution with Mono MSBuild
FROM mono:6.12 AS build
WORKDIR /src

# Copy solution, projects, and vendored NuGet/Kendo assemblies
COPY MovieDb.sln ./
COPY Libraries/ ./Libraries/
COPY packages/ ./packages/
COPY MovieDb/ ./MovieDb/
COPY MovieDb.DataAccess/ ./MovieDb.DataAccess/
COPY MovieDb.Models/ ./MovieDb.Models/
COPY MovieDb.Repository/ ./MovieDb.Repository/

# Build in Release mode
RUN msbuild MovieDb.sln /p:Configuration=Release /p:Platform="Any CPU" /m

# Stage 2: Runtime image with Mono + XSP4 web server
FROM mono:6.12 AS runtime

# Debian Buster (base of mono:6.12) moved to archive.debian.org; install mono-xsp4 and curl
RUN sed -i 's|deb.debian.org/debian|archive.debian.org/debian|g; s|security.debian.org/debian-security|archive.debian.org/debian-security|g; /buster-updates/d' /etc/apt/sources.list && \
    apt-get update && \
    apt-get install -y --no-install-recommends mono-xsp4 curl && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Copy compiled web application output and static assets/views
COPY --from=build /src/MovieDb/Global.asax ./
COPY --from=build /src/MovieDb/Web.config ./
COPY --from=build /src/MovieDb/favicon.ico ./
COPY --from=build /src/MovieDb/bin/ ./bin/
COPY --from=build /src/MovieDb/Content/ ./Content/
COPY --from=build /src/MovieDb/fonts/ ./fonts/
COPY --from=build /src/MovieDb/Scripts/ ./Scripts/
COPY --from=build /src/MovieDb/Views/ ./Views/

# Ensure poster uploads directory exists
RUN mkdir -p /app/Uploads/MoviePosters

# Enable case-insensitive file/path handling for Windows-authored ASP.NET MVC app
ENV MONO_IOMAP=all
ENV PORT=8080

EXPOSE 8080

CMD ["sh", "-c", "exec xsp4 --address 0.0.0.0 --port ${PORT} --root /app --nonstop"]
