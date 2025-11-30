# BloodLink API Documentation

This documentation provides an overview of the available API endpoints, request/response formats, and authentication for the BloodLink system.

---

## Authentication

### POST `/api/auth/login`
- **Description:** Log in with email and password.
- **Request Body:**
  ```json
  {
    "email": "user@example.com",
    "password": "yourPassword"
  }
  ```
- **Response:** User access details or error message.

### POST `/api/auth/logout`
- **Description:** Log out the currently authenticated user.
- **Request Body:** None.
- **Response:** Success or error.

---

## Users (Admin only)

### GET `/api/user`
- **Description:** Get all users.
- **Response:** Array of user objects.

### GET `/api/user/search?email=...`
- **Description:** Get user by email.
- **Response:** User object.

### DELETE `/api/user/{id}`
- **Description:** Deactivate a user (soft delete).
- **Response:** Success or error.

---

## Blood Banks

### GET `/api/bloodbank?Governorate=...`
- **Description:** Get all blood banks, optionally filtered by governorate.
- **Response:** Array of blood bank objects.

### POST `/api/bloodbank`
- **Roles:** `admin`
- **Description:** Add a new blood bank.
- **Request Body:** Follows `CreateBloodBankDto`.
- **Response:** Created blood bank or error.

#### Blood Bank Requests

##### Approving/Rejecting Requests
- **PUT** `/api/bloodbank/requests/{requestId}/approve`
- **PUT** `/api/bloodbank/requests/{requestId}/reject`
- **Roles:** `bloodbank`
- **Description:** Update the status of a blood request (approve or reject).
- **Response:** Success or error.

##### Get Requests for a Blood Bank
- **GET** `/api/bloodbank/requests`
- **Roles:** `bloodbank`
- **Description:** Get all requests for the blood bank.
- **Response:** Array of request objects.

##### Stock Management

###### Increase Stock
- **PUT** `/api/bloodbank/stock/increment`
- **Roles:** `bloodbank`
- **Body:** Follows `StockIncreamentDto`
- **Response:** Success or error.

###### Get Stock Details
- **GET** `/api/bloodbank/stock?bloodType=...`
- **Roles:** `bloodbank`
- **Description:** Get stock details for the blood bank, optionally by blood type.
- **Response:** Array of stock objects.

---

## Hospitals

### POST `/api/hospital`
- **Roles:** `admin`
- **Description:** Add a hospital.
- **Body:** Follows `CreateHospitalDto`
- **Response:** Created hospital or error.

### POST `/api/hospital/requests`
- **Roles:** `hospital`
- **Description:** Hospital requests blood.
- **Body:** Follows `CreateRequestDto`
- **Response:** Created request or error.

### GET `/api/hospital/requests`
- **Roles:** `hospital`
- **Description:** Get all requests made by the hospital.
- **Response:** Array of request objects.

### PUT `/api/hospital/requests/{requestId}/complete`
- **Roles:** `hospital`
- **Description:** Mark a request as completed.
- **Response:** Success or error.

### DELETE `/api/hospital/requests/{requestId}/cancle`
- **Roles:** `hospital`
- **Description:** Cancel a blood request.
- **Response:** Success or error.

---

## Data Transfer Objects (DTOs)

Please refer to the following for the shape of request/response bodies:
- **User:** `UserDTO`
- **Blood Bank:** `BloodBankDto`, `CreateBloodBankDto`, `UpdateBloodBankDto`
- **Hospital:** `HospitalDto`, `CreateHospitalDto`, `UpdateHospitalDto`
- **Request:** `RequestDto`, `CreateRequestDto`, `UpdateRequestDto`
- **Stock:** `StockDto`, `StockIncreamentDto`

*Detailed DTO structure available in the source files under `App.Application/DTOs/`.*

---

## Error Handling

Responses use consistent HTTP status codes:
- `200 OK` – Success
- `201 Created` – Resource created
- `400 Bad Request` – Invalid input
- `401 Unauthorized` – Not authenticated or invalid credentials
- `403 Forbidden` – Insufficient permissions
- `404 Not Found` – Resource does not exist
- `500 Internal Server Error` – Server error

*Error responses include status code and detailed message.*

---

## Authentication and Roles

- **Roles supported:** `admin`, `hospital`, `bloodbank`
- Endpoints are secured using these roles via `[Authorize(Roles = "role-name")]`.

---

## Notes

- All endpoints are prefixed with `/api/` as per controller routing.
- All requests and responses use JSON.
- Authorization tokens/cookies are expected for secured endpoints.

---
