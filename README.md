# Space Venue Manager (Concept)

This repository contains a WPF concept for the **Space** billiards & PlayStation venue. The UI and data bindings reflect the MVP scope: live timers, item sales, cash tracking, and daily statements in Egyptian Pounds (EGP) with an RTL toggle for Arabic support.

## Highlights

- **Live station timing & billing** for 3 pool tables and 2 PlayStations.
- **Item sales panel** for snacks and drinks.
- **Cash register summary** with opening, deposits, withdrawals, and expected closing.
- **Daily statement** snapshot and export action.
- **Space-themed styling** with dark nebula backgrounds and gold accents.

## Project Structure

```
SpaceVenueApp/
├── App.xaml
├── MainWindow.xaml
├── Models/
├── ViewModels/
└── SpaceVenueApp.csproj
```

## Notes

- This is a UI-focused concept. The data is currently seeded in `MainViewModel` and uses a `DispatcherTimer` to simulate real-time billing.
- Export functionality is stubbed (shows a confirmation dialog) and can be wired to actual PDF/CSV generation later.
- Currency formatting uses the `ar-EG` culture to show EGP formatting by default.

## Next Steps (Suggested)

- Add persistence (SQLite) for sessions, items, and cash transactions.
- Implement receipt printing and detailed session history.
- Add authentication roles for staff and managers.

