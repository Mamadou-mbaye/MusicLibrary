# Music Library Management

A self-hosted application to manage, analyze and improve a personal music library.

It integrates with [Navidrome](https://www.navidrome.org/) through the Subsonic API,
stores library data in its own PostgreSQL database, and can help identify missing,
inconsistent or duplicated metadata.

## Goal

The goal is not to replace Navidrome or a music player.
Navidrome remains the music server and streaming backend.

This application adds an additional layer for:

- Importing artists, albums and tracks from Navidrome
- Browsing and searching the music library
- Detecting missing or inconsistent metadata
- Identifying possible duplicate tracks or albums
- Generating statistics about the collection
- Optionally correcting audio file tags after user confirmation

## Tech Stack

### Backend

- ASP.NET Core Web API
- C# / .NET
- Dapper
- PostgreSQL
- MySqlConnector
- TagLib# for reading and writing audio metadata
- Docker / Docker Compose

### Frontend

- Vue 3
- Vite
- TypeScript
- Pinia
- pnpm
- axios

## Architecture

```text
Music files
   ↓
Navidrome
   ↓ Subsonic API
ASP.NET Core backend
   ↓ Entity Framework Core
PostgreSQL
   ↓ REST API
Vue frontend
```

The backend reads data from Navidrome using its Subsonic API.
It does not write directly to the Navidrome database.

Audio file tags are modified only after an explicit user confirmation.

## License

This project is licensed under the [MIT License](LICENSE).

## Features

### MVP

- [x] Connect to Navidrome using the Subsonic API
- [x] Import artists, albums and tracks
- [x] Store library data in PostgreSQL
- [x] Browse artists, albums and tracks
- [x] Search by title, artist, album and genre
- [ ] Detect tracks with missing metadata
- [ ] Detect possible duplicates

### Later

- [ ] Listening statistics
- [ ] Wishlist for albums or artists to explore
- [ ] Metadata correction workflow
- [ ] Tag editing with TagLib#
- [ ] User authentication
- [ ] Docker Compose deployment

## Development

### Backend

```bash
cd src/MusicLibrary.Api
dotnet restore
dotnet run
```

### Frontend

```bash
cd frontend
vp install
vp dev
```

## Project Status

🚧 Work in progress
