# HotDate

HotDate is a simple API for calculating business days, practicing dotnet core and learning xunit. 

## Features

### Business Day Calculation
- Get Business Days Between Dates
- Supports Annual Holidays (eg Australia Day), with optional Rollover (eg New Years Day)
- Supports Rule-based Holidays (eg Queens Birthday)
- Supports Adhoc holidays (eg AFL Grand Final Day)

### Holiday Configuration (POC Only)
- View Adhoc Holidays
- Create New Adhoc Holidays

## Assumptions and Limitations
- Aussie Aussie Aussie: supports only Australian holidays
- What's a timezone?  let's just assume we're all living in UTC-opia
- Public holidays are forever, long live public holidays (no start or end dates)

## Unimplemented / ToDo
- CRUD operations for all holiday types
- Making HolidayService Typed
- Catholic Calendar based holidays (eg Easter holidays)

## Installation
- Run from Api folder with "dotnet run"
- Test from HotDate or Api.Tests folder with "dotnet test"
- You may need to trust dev SSL certs by running "dotnet dev-certs https --trust"

Github: https://github.com/whatcouldgoright/HotDate

Swagger link: https://localhost:5001/swagger/index.html