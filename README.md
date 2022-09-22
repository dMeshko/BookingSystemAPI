# BookingSystemAPI
challenge task - testing inputs

### Requesting the token
```
curl -X 'POST' \
  'https://localhost:7226/api/authorize' \
  -H 'accept: application/json' \
  -d ''
```

### Searching
##### Search last minute hotels (hotels for the next 45 days)
```
curl -X 'GET' \
  'https://localhost:7226/api/search?Destination=SKP&FromDate=2022-09-22T17%3A32%3A28Z&ToDate=2022-09-27T17%3A32%3A28Z' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI'
```

##### Search hotels
```
curl -X 'GET' \
  'https://localhost:7226/api/search?Destination=SKP&FromDate=2023-09-22T17%3A32%3A28Z&ToDate=2023-09-27T17%3A32%3A28Z' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI'
```

##### Search hotels and flights
```
curl -X 'GET' \
  'https://localhost:7226/api/search?Destination=SKP&DepartureAirport=OSL&FromDate=2023-09-22T17%3A32%3A28Z&ToDate=2023-09-27T17%3A32%3A28Z' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI'
```

##### Search last minute hotels (hotels for the next 45 days)
```
curl -X 'GET' \
  'https://localhost:7226/api/search?Destination=SKP&DepartureAirport=OSL&FromDate=2022-09-22T17%3A32%3A28Z&ToDate=2022-09-27T17%3A32%3A28Z' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI'
```

### Booking
##### Book the last minute hotel
```
curl -X 'POST' \
  'https://localhost:7226/api/book' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI' \
  -H 'Content-Type: application/json' \
  -d '{
  "optionCode": "8626",
  "searchReq": {
    "destination": "SKP",
    "departureAirport": "",
    "fromDate": "2022-09-22T17:32:28Z",
    "toDate": "2022-09-27T17:32:28Z"
  }
}'
```

##### Book the hotel
```
curl -X 'POST' \
  'https://localhost:7226/api/book' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI' \
  -H 'Content-Type: application/json' \
  -d '{
  "optionCode": "8626",
  "searchReq": {
    "destination": "SKP",
    "departureAirport": "",
    "fromDate": "2023-09-22T17:32:28Z",
    "toDate": "2023-09-27T17:32:28Z"
  }
}'
```

##### Book the flight
```
curl -X 'POST' \
  'https://localhost:7226/api/book' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI' \
  -H 'Content-Type: application/json' \
  -d '{
  "optionCode": "SK 461",
  "searchReq": {
    "destination": "SKP",
    "departureAirport": "OSL",
    "fromDate": "2022-09-22T17:32:28Z",
    "toDate": "2022-09-27T17:32:28Z"
  }
}'
```

##### Confirm the booking is made
```
curl -X 'GET' \
  'https://localhost:7226/api/book/0vileg' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI'
```

### Check the booking status
##### Check the booking status
```
curl -X 'GET' \
  'https://localhost:7226/api/status?BookingCode=0vileg' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0YmRhMmVhNS00MWVhLTQyMjctYThjNi03MWRkNmJkZmQwOTYiLCJuYmYiOjE2NjM4NDMwMDQsImV4cCI6MTY2Mzg3OTAwNCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNiIsImF1ZCI6ImJvb2tpbmdfc3lzdGVtLmFwaSJ9.UZJ5urKkzj1BwEDiph_QlkoblyEyn1pPN3jO51LMSLI'
```