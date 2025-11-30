# BloodLink

## Comprehensive Documentation

### Installation Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/marwa-gameil/BloodLink.git
   ```
2. Navigate to the project directory:
   ```bash
   cd BloodLink
   ```
3. Install the required dependencies:
   ```bash
   npm install
   ```

### System Requirements
- Operating System: Windows/Linux/MacOS
- Node.js: v14 or higher
- Database: MongoDB (version x.x)
  
### Configuration Instructions
1. Create a `.env` file in the root directory of the project.
2. Add the necessary environment variables:
   ```
   DB_URI=mongodb://localhost:27017/bloodlink
   PORT=3000
   SECRET_KEY=your_secret_key
   ```

### Execution Guide
- Start the application:
  ```bash
  npm start
  ```
- The application runs on `http://localhost:3000`.

### API Documentation
- Base URL: `http://localhost:3000/api`
- Example Endpoints:
  - GET `/api/v1/users` - Retrieve users
  - POST `/api/v1/users` - Create a new user

### Project Structure
```
/BloodLink
  ├── /src
  │   ├── /controllers   # Controller logic
  │   ├── /models        # Database models
  │   ├── /routes        # API routes
  │   └── /middlewares    # Custom middleware
  ├── /config            # Configuration files
  ├── .env               # Environment variables
  ├── package.json       # Project metadata and dependencies
  └── README.md          # Documentation
```

### Deployment Information
1. Ensure all environment variables are set in the production environment.
2. Run the following commands for deployment:
   ```bash
   npm install --production
   npm start
   ```
3. For cloud deployment, consider using platforms like Heroku, AWS, or DigitalOcean.
