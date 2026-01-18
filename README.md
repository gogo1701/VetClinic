# VetClinic Management System

A comprehensive web application for managing a veterinary clinic's daily operations, including pet records, owner information, appointments, and medical records.

## Overview

VetClinic is a full-featured ASP.NET Core Razor Pages application designed to streamline veterinary clinic management. It provides an intuitive interface for tracking pets, managing appointments, maintaining medical records, and facilitating pet adoptions.

## Features

### Pet Management

- **Register & Track Pets**: Add pets with detailed information including species, breed, date of birth, weight, color, and microchip number
- **Pet Profiles**: View comprehensive pet information including owner details, appointment history, and medical records
- **Adoption System**: Track adoption status and manage available pets for adoption
- **Search & Filter**: Search pets by name, breed, owner, species, or adoption status

### Owner Management

- **Owner Database**: Maintain contact information for pet owners including name, email, phone, and address
- **Owner Profiles**: View all pets associated with each owner
- **Contact Management**: Easy access to owner contact details for communication

### Appointment Scheduling

- **Schedule Appointments**: Book appointments with pet, date/time, reason, and notes
- **Appointment Status Tracking**: Track appointment status (Scheduled, Confirmed, In Progress, Completed, Cancelled)
- **Dashboard Integration**: View today's and upcoming appointments at a glance
- **Quick Actions**: Schedule appointments directly from pet profiles

### Medical Records

- **Comprehensive Records**: Document diagnoses, treatments, medications, and veterinarian notes
- **Cost Tracking**: Record visit costs for billing purposes
- **Search & Filter**: Filter medical records by pet name, diagnosis, treatment, or date range
- **Visit History**: Access complete medical history for each pet

### Dashboard

- **Quick Statistics**: View total pets, adopted pets, owners, and today's appointments
- **Recent Activity**: See recently registered pets
- **Upcoming Appointments**: Monitor scheduled appointments
- **Quick Actions**: Fast access to common tasks

## Technology Stack

- **Framework**: ASP.NET Core 9.0
- **UI Pattern**: Razor Pages
- **Database**: SQLite with Entity Framework Core 9.0
- **Frontend**: Bootstrap 5 with Bootstrap Icons
- **Architecture**: Repository/Service pattern with dependency injection

## Project Structure

```
VetClinic.Web/
├── Data/                  # Database context
│   └── ApplicationDbContext.cs
├── Models/                # Domain entities
│   ├── Pet.cs
│   ├── Owner.cs
│   ├── Appointment.cs
│   └── MedicalRecord.cs
├── Pages/                 # Razor Pages
│   ├── Index.cshtml       # Dashboard
│   ├── Pets/              # Pet management pages
│   ├── Owners/            # Owner management pages
│   ├── Appointments/      # Appointment scheduling pages
│   ├── MedicalRecords/    # Medical records pages
│   └── Shared/            # Shared layouts and partials
├── Services/              # Business logic layer
│   ├── Interfaces/
│   │   ├── IPetService.cs
│   │   ├── IOwnerService.cs
│   │   ├── IAppointmentService.cs
│   │   └── IMedicalRecordService.cs
│   ├── PetService.cs
│   ├── OwnerService.cs
│   ├── AppointmentService.cs
│   └── MedicalRecordService.cs
├── Migrations/            # EF Core migrations
├── wwwroot/               # Static files (CSS, JS, images)
│   ├── css/
│   ├── js/
│   └── lib/               # Client libraries (Bootstrap, jQuery)
├── Program.cs             # Application entry point
├── appsettings.json       # Configuration
└── VetClinic.Web.csproj   # Project file
```

### Architecture Pattern

The application follows a **layered architecture**:

1. **Presentation Layer** (Razor Pages): Handles user interface and user interactions
2. **Service Layer** (Services): Contains business logic and data validation
3. **Data Access Layer** (ApplicationDbContext): Manages database operations using Entity Framework Core
4. **Domain Layer** (Models): Defines the core entities and their relationships

**Benefits:**

- Separation of concerns
- Easier testing and maintenance
- Dependency injection for loose coupling
- Scalability and flexibility

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A code editor (Visual Studio 2022, VS Code, or JetBrains Rider)
- Git (optional)

### Installation & Running

#### Step 1: Clone or Download the Repository

```bash
# Clone the repository (if using Git)
git clone <repository-url>

# Or download and extract the ZIP file
```

#### Step 2: Navigate to the Project Directory

```bash
cd VetClinic/VetClinic.Web
```

#### Step 3: Restore NuGet Packages

Restore all required dependencies:

```bash
dotnet restore
```

This will install:

- Entity Framework Core and SQLite provider
- Bootstrap and jQuery libraries
- All other required NuGet packages

#### Step 4: Apply Database Migrations

The application uses SQLite, so the database file will be created automatically. Apply migrations to set up the database schema:

```bash
dotnet ef database update
```

**Note**: If you don't have the EF Core tools installed globally, install them first:

```bash
dotnet tool install --global dotnet-ef
```

This will create:

- `VetClinic.db` - SQLite database file in the project root
- All required tables (Pets, Owners, Appointments, MedicalRecords, AspNetUsers, etc.)

#### Step 5: Build the Application (Optional)

```bash
dotnet build
```

This step is optional as `dotnet run` will build automatically, but it's useful to check for compilation errors.

#### Step 6: Run the Application

Start the development server:

```bash
dotnet run
```

Or use the watch mode for automatic reloading during development:

```bash
dotnet watch run
```

#### Step 7: Access the Application

Open your browser and navigate to:

- **HTTPS**: `https://localhost:5001` (recommended)
- **HTTP**: `http://localhost:5000`

You should see the VetClinic home page with the dashboard.

### First-Time Setup

#### Add Sample Data

For the best experience, populate the system in this order:

**a) Add Owners First**

- Navigate to **Owners** → **Create New Owner**
- Add at least 2-3 owners with complete contact information
- This allows you to associate pets with their owners

**b) Add Pets**

- Go to **Pets** → **Add New Pet**
- Create several pets and assign them to owners
- Include various species (dogs, cats, birds, etc.) to see the full functionality
- Try leaving some pets without owners to test the adoption feature

**c) Schedule Appointments**

- Navigate to **Appointments** → **Schedule Appointment**
- Create appointments for today and future dates
- Test different statuses (Scheduled, Confirmed, etc.)

**d) Add Medical Records**

- Go to **Medical Records** → **Add Record**
- Document some sample medical visits
- This populates the pet's medical history

#### Explore the Dashboard

- Return to the home page to see your dashboard populated with statistics
- View recent activities and today's appointments
- Use the quick action buttons for common tasks

## Usage Guide

### Adding a New Pet

1. Navigate to **Pets** → **Add New Pet**
2. Fill in required information:

- Name, Species, Breed
- Date of Birth
- Weight
- Optional: Color, Microchip Number, Owner

3. Click **Create** to save

### Scheduling an Appointment

1. Go to **Appointments** → **Schedule Appointment**
2. Select a pet from the dropdown
3. Choose date and time
4. Enter the reason for visit
5. Add optional notes
6. Click **Schedule**

### Recording Medical Information

1. Navigate to **Medical** → **Add Record**
2. Select the pet
3. Enter visit date, diagnosis, treatment, and medications
4. Add veterinarian notes and cost if applicable
5. Click **Save**

### Managing Adoptions

1. View available pets: **Pets** → Filter by "Available for Adoption"
2. To mark a pet as adopted:

- Go to the pet's profile
- Click **Edit**
- Assign an owner
- The system automatically marks the pet as adopted

## Database Schema

### Key Entities

#### **Pets**

Core entity representing animals in the system.

**Fields:**

- `Id` (int, PK): Unique identifier
- `Name` (string, required): Pet's name
- `Species` (string, required): Type of animal (Dog, Cat, Bird, etc.)
- `Breed` (string, required): Specific breed
- `DateOfBirth` (DateTime, required): Birth date
- `Weight` (decimal): Current weight
- `Color` (string, optional): Fur/feather color
- `MicrochipNumber` (string, optional): Microchip ID
- `IsAdopted` (bool): Adoption status
- `OwnerId` (int, FK, optional): Reference to owner
- `RegistrationDate` (DateTime): When pet was added to system

#### **Owners**

Contact information for pet owners.

**Fields:**

- `Id` (int, PK): Unique identifier
- `Name` (string, required): Full name
- `Email` (string, required): Email address
- `Phone` (string, required): Contact number
- `Address` (string, optional): Physical address

#### **Appointments**

Scheduled veterinary visits.

**Fields:**

- `Id` (int, PK): Unique identifier
- `PetId` (int, FK, required): Reference to pet
- `AppointmentDate` (DateTime, required): Scheduled date and time
- `Reason` (string, required): Purpose of visit
- `Notes` (string, optional): Additional information
- `Status` (enum): Current status (Scheduled, Confirmed, In Progress, Completed, Cancelled)

#### **MedicalRecords**

Comprehensive medical history and visit records.

**Fields:**

- `Id` (int, PK): Unique identifier
- `PetId` (int, FK, required): Reference to pet
- `VisitDate` (DateTime, required): Date of visit
- `Diagnosis` (string, required): Medical diagnosis
- `Treatment` (string, required): Treatment provided
- `Medications` (string, optional): Prescribed medications
- `VeterinarianNotes` (string, optional): Vet's notes
- `Cost` (decimal, optional): Visit cost

### Relationships

- **Owner → Pets**: One-to-Many (One owner can have multiple pets)
- **Pet → Appointments**: One-to-Many (One pet can have multiple appointments)
- **Pet → MedicalRecords**: One-to-Many (One pet can have multiple medical records)
- **Pet → Owner**: Many-to-One (Optional, for tracking ownership)

### Database Technology

**SQLite** is used as the database engine:

- Lightweight and serverless
- No installation required
- Database stored as a single file (`VetClinic.db`)
- Perfect for development and small to medium deployments
- Can be easily migrated to SQL Server or PostgreSQL for production

## UI Features

- **Responsive Design**: Mobile-friendly Bootstrap 5 interface
- **Sidebar Navigation**: Quick access to all sections
- **Status Badges**: Visual indicators for appointment status and adoption status
- **Search & Filter**: Advanced filtering on all list pages
- **Success Messages**: Toast notifications for user actions
- **Dashboard Cards**: Color-coded statistics cards
