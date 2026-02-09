# Space Venue Manager (Concept)

This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The app reflects the MVP scope: live timers, item management, cash transactions, and daily/monthly financial statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.

## Highlights

- **Live station timing & billing** for 3 pool tables and 2 PlayStations.
- **Customer session tracking** with start/pause/stop/reset and CSV export.
- **Item catalog CRUD** and quick sales recording.
- **Cash register summary** with deposit/withdrawal tracking.
- **Daily and monthly statements** with CSV exports.
- **Space-themed styling** with dark nebula backgrounds and gold accents.

## Project Structure

```
SpaceVenueApp/
├── App.xaml
├── MainWindow.xaml
├── Converters/
├── Models/
├── Services/
├── ViewModels/
└── SpaceVenueApp.csproj
```

## Notes

- SQLite storage is created in the user profile under `LocalApplicationData/SpaceVenueApp`.
- Exported CSV files are written to `LocalApplicationData/SpaceVenueApp/Reports`.
- Currency formatting uses the `ar-EG` culture with Arabic numerals when RTL is enabled.

## Next Steps (Suggested)

- Add receipt printing and PDF exports.
- Integrate authentication roles for staff and managers.
- Add rich reporting filters and analytics dashboards.

