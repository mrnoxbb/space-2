# Space Venue Manager (Concept)

<<<<<<< HEAD
This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The app reflects the MVP scope: live timers, item management, cash transactions, and daily/monthly financial statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.
=======
This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The UI and data bindings reflect the MVP scope: live timers, item sales, cash tracking, and daily statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.
>>>>>>> main

## Highlights

- **Live station timing & billing** for 3 pool tables and 2 PlayStations.
<<<<<<< HEAD
- **Customer session tracking** with start/pause/stop/reset and CSV export.
- **Item catalog CRUD** and quick sales recording.
- **Cash register summary** with deposit/withdrawal tracking.
- **Daily and monthly statements** with CSV exports.
=======
- **Item sales panel** for snacks and drinks.
- **Cash register summary** with opening, deposits, withdrawals, and expected closing.
- **Daily statement** snapshot and export action.
>>>>>>> main
- **Space-themed styling** with dark nebula backgrounds and gold accents.

## Project Structure

```
SpaceVenueApp/
├── App.xaml
├── MainWindow.xaml
<<<<<<< HEAD
├── Converters/
├── Models/
├── Services/
=======
├── Models/
>>>>>>> main
├── ViewModels/
└── SpaceVenueApp.csproj
```

## Notes

<<<<<<< HEAD
- SQLite storage is created in the user profile under `LocalApplicationData/SpaceVenueApp`.
- Exported CSV files are written to `LocalApplicationData/SpaceVenueApp/Reports`.
- Currency formatting uses the `ar-EG` culture with Arabic numerals when RTL is enabled.

## Next Steps (Suggested)

- Add receipt printing and PDF exports.
- Integrate authentication roles for staff and managers.
- Add rich reporting filters and analytics dashboards.
=======
- This is a UI-focused concept. The data is currently seeded in `MainViewModel` and uses a `DispatcherTimer` to simulate real-time billing.
- Export functionality is stubbed (shows a confirmation dialog) and can be wired to actual PDF/CSV generation later.
- Currency formatting uses the `ar-EG` culture to show EGP formatting by default.

## Next Steps (Suggested)

- Add persistence (SQLite) for sessions, items, and cash transactions.
- Implement receipt printing and detailed session history.
- Add authentication roles for staff and managers.
>>>>>>> main

