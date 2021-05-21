# HotDate

Run from Api folder with "dotnet run"
Test from HotDate or Api.Tests folder wtih "dotnet test"
You may need to trust dev SSL certs by running "dotnet dev-certs https --trust"

Assumptions and Limitations:
- Aussie Aussie Aussie: supports only Australian holidays
- What's a timezone?  let's just assume we're all living in UTC-opia
- Public holidays are forever, long live public holidays (no start or end dates)

Features:
- Get Business Days Between Dates (excluding weekends and holidays)
- View Adhoc Holidays
- Create New Adhoc Holidays

Unimplemented / ToDo:
- CRUD operations for all holiday types
- Making HolidayService Typed

Github: https://github.com/whatcouldgoright/HotDate

Swagger link: https://localhost:5001/swagger/index.html