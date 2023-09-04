using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HelathCare53
{
    class Program
    {
        // List to store patient profiles
        static List<Patient> patients = new List<Patient>();

        // List to store doctor profiles
        static List<Doctor> doctors = new List<Doctor>();

        // List to store staff profiles 
        static List<Staff> staffs = new List<Staff>();

        // List to store appoinments
        static List<Appointment> appointments =new List<Appointment>();

        // List of locations
        static List<Location> locations = new List<Location>();


        class Patient
        {
            public string Name {get; set;}
            public DateTime DOB {get; set;}
            public string HealthcareNumber {get; set;}
            public string Password {get; set;}
            public string PhoneNumber{get; set;}
            public DateTime BCHealthcardExpiryDate {get; set;}



            public Patient(
                string name, string dob, string healthCareNum,
                string phoneNumber, string password, DateTime bc
            )
            {
                Name = name;
                DOB = DateTime.Parse(dob);
                HealthcareNumber = healthCareNum;
                PhoneNumber = phoneNumber;
                Password = password;
                BCHealthcardExpiryDate = bc;
            }
        }

        class Doctor
        {
            public string Name {get; set;}
            public string DoctorId {get; set;}

            public Doctor(string name, string id)
            {
                Name = name;
                DoctorId = id;
            }
        }

        class Staff
        {
            public string Name {get; set;}
            public string StaffId {get; set;}

            public Staff(string name, string id)
            {
                Name = name;
                StaffId = id;
            }
        }

        enum IllnessCategory
        {
            General_Check_Up,
            Dermatology,
            Orthopedics, 
            Cardiology
        }

        class Appointment
        {
            public Patient Patient {get; set;}
            public Doctor Doctor {get; set;}
            public DateTime DateTime {get; set;}
            public Inquiry Inquiry {get; set;}
            public IllnessCategory Illness{get ; set;}
            public Result Result {get; set;}

            public Appointment(Patient patient, Doctor doctor, DateTime dateTime, Inquiry inquiry, IllnessCategory illness)
            {
                Patient = patient;
                Doctor = doctor;
                DateTime = dateTime;
                Inquiry = inquiry;
                Illness = illness;
            }
        }

        class Inquiry
        {
            public string FullName {get; set;}
            public DateTime DateOfBirth {get; set;}
            public string Address {get; set;}
            public string Email {get; set;}
            public string PhoneNumber {get; set;}
            public string EmergencyPhoneNumber {get; set;}
            public string Symptoms {get; set;}
            public string DurationOfSymptoms {get; set;}
            public string PreviousMedicalHistory {get; set;}
            public string CurrentMedications {get; set;}
            public string Allergies {get; set;}
            public string AdditonalComments {get; set;}
            public string PatientExplanation {get; set;}
            public DateTime BCHealthcardExpiryDate {get; set;}
        
            public Inquiry(
                string fullName, DateTime dateOfBirth, string address,
                string email, string phoneNumber, string emergencyPhoneNumber,
                string symptoms, string durationOfSymptoms, string previousMedicalHistory,
                string currentMedications, string allergies, string additonalComments,
                string patientExplanation, DateTime bCHealthcardExpiryDate
            )
            {
                FullName = fullName;
                DateOfBirth = dateOfBirth;
                Address = address;
                Email = email;
                PhoneNumber = phoneNumber;
                EmergencyPhoneNumber = emergencyPhoneNumber;
                Symptoms = symptoms;
                DurationOfSymptoms = durationOfSymptoms;
                PreviousMedicalHistory = previousMedicalHistory;
                CurrentMedications = currentMedications;
                Allergies = allergies;
                AdditonalComments = additonalComments;
                PatientExplanation = patientExplanation;
                BCHealthcardExpiryDate = bCHealthcardExpiryDate;
            }
        }

        class Location
        {
            public string Name {get; set;}
            public string Address {get; set;}
            public string Phone {get; set;}

            public Location(string name, string address, string phone)
            {
                Name = name;
                Address = address;
                Phone = phone;
            }
        }        

        class Result {
            public string Details {get; set;}

            public Result(string details)
            {
                Details = details;
            }
        }
    

        static void Main(string[] args)
        {
            // Initialize data
            Initialize();

            while(true)
            {
                Console.Clear(); // Clear Screen

                // Login options
                
                Console.WriteLine("1. Patient Login");
                Console.WriteLine("2. Doctor Login");
                Console.WriteLine("3. Staff Login");
                Console.WriteLine("0. Exit");

                int choice = Int32.Parse(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        PatientLogin();
                        break;
                    
                    case 2:
                        DoctorLogin();
                        break;
                    
                    case 3:
                        StaffLogin();
                        break;
                    
                    case 0:
                        Console.WriteLine("Exiting application...");
                        return;
                    
                    default:
                        Console.WriteLine("Invalid choice.");
                        continue;
                }
            }
        }

        static void PatientLogin()
        {
            Console.Write("Please, enter healthcare number: ");
            string healthCareNum = Console.ReadLine();

            // Validate healthcare number
            if (!ValidateHealthCareNumber(healthCareNum))
            {
                Console.WriteLine("Invalid healthcare number");
                return;
            }

            // Exiting patient
            if (IsExistingPatient(healthCareNum))
            {
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                Patient patient = GetPatient(healthCareNum);
                if (patient.Password != password)
                {
                    Console.WriteLine("Invalid password!");
                
                    AskRecoveryOption();
                    return;
                }

                if (IsBCExpire(patient))
                {
                    Console.WriteLine("Your Health Card has been expired!");
                    return;
                }

                PatientMenu(patient);
            }
            else // New patient
            {
                Console.WriteLine("Creating new patient profile...");
                CreatePatientProfile(healthCareNum);
            }
        }

        static bool IsBCExpire(Patient patient)
        {
            DateTime now = DateTime.Now;
            
            if (now < patient.BCHealthcardExpiryDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static void AskRecoveryOption(){
            Console.WriteLine("Do you want to send the recover password to Your phone number?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            int answer =  Int32.Parse(Console.ReadLine());

            if (answer==1)
            {
                Console.Write("Your recovery code sent!");
            }
            else return;
        }

        static void PatientMenu(Patient patient)
        {
            while(true)
            {
                // Patient menu option
                Console.WriteLine("1. Book Appoinment");
                Console.WriteLine("2. Update Profile");
                Console.WriteLine("3. Review Appoitnents");
                Console.WriteLine("0. Logout");

                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        BookAppoinment(patient);
                        break;
                                        
                    case 2:
                        UpdateProfile(patient);
                        break;
                    
                    case 3:
                        ViewAppointmentsForPatient(patient);
                        break;

                    case 0:
                        Console.WriteLine("Logging out...");
                        return;
                
                    default:
                        Console.WriteLine("Invalid choice ");
                        continue;
                }
            }
        }
        static void ViewAppointmentsForPatient(Patient patient){

             for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].Patient == patient  ){
                    Console.WriteLine($"{appointments[i].Doctor} : {appointments[i].Result}");
                }
            }
        }

        static void DoctorLogin()
        {
            // Doctor login logic here by calling its own external function within this function
            Console.Write("Please, enter your ID: ");
            string id = Console.ReadLine();

            // Exiting doctor
            if (IsExistingDoctor(id))
            {
                Doctor doctor = GetDoctor(id);
                DoctorMenu(doctor);
            }
            else
            {
                Console.WriteLine("Invalid ID, try again...");
            }
        }

        // Check if patient exists
        static bool IsExistingDoctor(string id)
        {
            foreach (Doctor doctor in doctors)
            {
                if (doctor.DoctorId == id)
                {
                    return true;
                }
            }
            return false;
        }
    
        static Doctor GetDoctor(string id)  // Get doctor object
        {
            foreach (Doctor doctor in doctors)
            {
                if (doctor.DoctorId == id)
                {
                    return doctor;
                }
            }
            return null;
        }

        
        static void StaffLogin()
        {
             // staff login logic here by calling its own external function within this function
            Console.Write("Please, enter your ID: ");
            string id = Console.ReadLine();

            // Exiting Straff
            if (IsExistingStaff(id))
            {
                Staff staff = GetStaff(id);
                StaffMenu(staff);
            }
            else
            {
                Console.WriteLine("Invalid ID, try again...");
            }
            
        }
        static bool IsExistingStaff(string id)
        {
            foreach (Staff staff in staffs)
            {
                if (staff.StaffId == id)
                {
                    return true;
                }
            }
            return false;
        }
        static Staff GetStaff(string id)  // Get staff object
        {
            foreach (Staff staff in staffs)
            {
                if (staff.StaffId == id)
                {
                    return staff;
                }
            }
            return null;
        }

        // Initialize sample data
        static void Initialize()
        {
            // Create sample patients
            Patient omid = new Patient("Omid Najjar", "1992-09-25", "BC09155672", "0000", "", DateTime.Parse("2025-09-03"));
            omid.Password = "omiD#123";

            // Create the first patient object
            Patient patient1 = new Patient(
                "John Doe",
                "1990-05-15",
                "BC09155612",
                "1234567890",
                "mypassworD1@",
                new DateTime(2024, 12, 31)
            );

            // Create the second patient object
            Patient patient2 = new Patient(
                "Jane Smith",
                "1985-08-10",
                "BC09155673",
                "0987654321",
                "anotherpassworD1@",
                new DateTime(2023, 10, 15)
            );

            // Create the third patient object
            Patient patient3 = new Patient(
                "Alice Johnson",
                "1995-03-25",
                "BC09155675",
                "5555555555",
                "securepassworD1@",
                new DateTime(2022, 06, 30)
            );

            // Create the fourth patient object
            Patient patient4 = new Patient(
                "Bob Thompson",
                "1982-11-05",
                "BC09155679",
                "6666666666",
                "p@ssw0rdA",
                new DateTime(2025, 04, 15)
            );

            // Create the fifth patient object
            Patient patient5 = new Patient(
                "Sarah Wilson",
                "1998-07-20",
                "BC09155688",
                "7777777777",
                "strongPassword1@",
                new DateTime(2023, 07, 01)
            );

            // Add them to patients List
            patients.Add(omid);
            patients.Add(patient1);
            patients.Add(patient2);
            patients.Add(patient3);
            patients.Add(patient4);
            patients.Add(patient5);
            
            // Create sample doctors below
            Doctor Ali = new Doctor("Dr. Ali Najjar", "D0915111");
            Doctor Arta = new Doctor("Dr. Eli Najjar", "09150090");

            // Add doctors to Doctor's List
            doctors.Add(Ali);
            doctors.Add(Arta);


            // Create sample 
            Inquiry inquiry1 = new Inquiry(
                "John Doe",
                new DateTime(1990, 5, 15),
                "123 Main St",
                "john.doe@example.com",
                "1234567890",
                "9876543210",
                "Fever, cough",
                "2 days",
                "None",
                "Ibuprofen",
                "None",
                "No additional comments",
                "I'm feeling unwell",
                new DateTime(2024, 12, 31)
            );

            // Create the second object
            Inquiry inquiry2 = new Inquiry(
                "Jane Smith",
                new DateTime(1985, 8, 10),
                "456 Elm St",
                "jane.smith@example.com",
                "0987654321",
                "0123456789",
                "Sore throat",
                "1 week",
                "Asthma",
                "Ventolin",
                "None",
                "No special instructions",
                "I have a history of respiratory issues",
                new DateTime(2023, 10, 15)
            );

            // Create the third object
            Inquiry inquiry3 = new Inquiry(
                "Alice Johnson",
                new DateTime(1995, 3, 25),
                "789 Oak St",
                "alice.johnson@example.com",
                "5555555555",
                "6666666666",
                "Headache",
                "3 days",
                "Migraine",
                "Tylenol",
                "None",
                "Please call me for any updates",
                "I frequently experience migraines",
                new DateTime(2025, 6, 30)
            );
            // Create sample appointments
            Appointment appointment1 = new Appointment(omid, Ali, new DateTime(2023, 9, 10, 10, 30, 0), inquiry1, IllnessCategory.Orthopedics);
            Appointment appointment2 = new Appointment(patient1, Arta, new DateTime(2023, 10, 10, 16, 0, 0), inquiry2, IllnessCategory.Orthopedics);

            // Add to appointments List
            appointments.Add(appointment1);
            appointments.Add(appointment2);  

            // Create the first location object
            Location location1 = new Location(
                "Hospital A",
                "123 Main St",
                "1234567890"
            );

            // Create the second location object
            Location location2 = new Location(
                "Hospital B",
                "456 Elm St",
                "0987654321"
            );

            // Create the third location object
            Location location3 = new Location(
                "Hospital C",
                "789 Oak St",
                "5555555555"
            );

            // Create the fourth location object
            Location location4 = new Location(
                "Hospital D",
                "321 Pine St",
                "7777777777"
            );

            // Create the fifth location object
            Location location5 = new Location(
                "Hospital E",
                "987 Cedar St",
                "9999999999"
            );

            // Create the sixth location object
            Location location6 = new Location(
                "Hospital F",
                "654 Walnut St",
                "1111111111"
            );

            // Create the seventh location object
            Location location7 = new Location(
                "Hospital G",
                "234 Maple St",
                "2222222222"
            );

            // Create the eighth location object
            Location location8 = new Location(
                "Hospital H",
                "876 Birch St",
                "3333333333"
            );

            // Create the ninth location object
            Location location9 = new Location(
                "Hospital I",
                "543 Oak St",
                "4444444444"
            );

            // Create the tenth location object
            Location location10 = new Location(
                "Hospital X",
                "546 Oak St",
                "4444444441"
            );

            // and add locations to the List
            locations.Add(location1);
            locations.Add(location2);
            locations.Add(location3);
            locations.Add(location4);
            locations.Add(location5);
            locations.Add(location6);
            locations.Add(location7);
            locations.Add(location8);
            locations.Add(location9);
            locations.Add(location10);
            
        }
        
        // Validate healthcare number in function below
        static bool ValidateHealthCareNumber(string number)
        {
            if (number.Length != 10) // I put some condition that if the healthcare number is less than 10 nimbers it is a wrong format.
            {
                return false;
            }
            if (!number.StartsWith("BC")) // if it is not start with BC for instance, it is again wrong format
            {
                return false;
            }
            int result;
            if (!Int32.TryParse(number.Substring(2), out result))
            {
                return false;
            }
            return true;
        }

        // Check if patient exists
        static bool IsExistingPatient(string healthCareNum)
        {
            foreach (Patient patient in patients)
            {
                if (patient.HealthcareNumber == healthCareNum)
                {
                    return true;
                }
            }
            return false;
        }
    
        static Patient GetPatient(string healthCareNum)  // Get patint object
        {
                foreach (Patient patient in patients)
                {
                    if (patient.HealthcareNumber == healthCareNum)
                    {
                        return patient;
                    }
                }
                 return null;
        }

        // Create patient profile
        static void CreatePatientProfile(string healthCareNum)
        {
            string password = "";
            string phoneNumber = "";

            Console.Write("Please, enter your name (patient): ");
            string name = Console.ReadLine();

            Console.Write("Please, enter your date of birth: ");
            string dob = Console.ReadLine();

            Console.Write("Please enter password: ");
            while(true)
            {
                password = Console.ReadLine();

                if(ValidatePassword(password)){
                    break;
                }
                else 
                {
                    Console.Write("your password must included combination of lower case and upper case and number and special characters, also it must be at least 8 charachter and maximum 16 character");
                }

            }

            while (true)
            {
                Console.Write("Please enter your recover phone number:");
                phoneNumber = Console.ReadLine();
                
                if(ValidatePhoneNumber(phoneNumber)){
                    break;
                }
                else {
                    Console.Write("your input as phone number is invalid");
                }
            }

            Console.WriteLine("Please enter your healthcard expration date");
            DateTime bcexpire = DateTime.Now;    
            while (true)
            {
                try
                {
                    string bchealthcardexpirydate = Console.ReadLine();
                    bcexpire = DateTime.Parse(bchealthcardexpirydate);    
                    break;              
                }
                catch (System.Exception)
                {
                    Console.Write("Please, use the right format (YYYY-MM-DD): ");
                }
            }
            
            Patient patient = new Patient(name, dob, healthCareNum, phoneNumber, password, bcexpire);
            patients.Add(patient);

            Console.WriteLine("Patient profile created successfully");
        }

        static bool ValidatePhoneNumber(string phoneNumber){

                string pattern = @"^09[0-9]{9}$" ; 
                return Regex.IsMatch(phoneNumber ,pattern );
        }

        static bool ValidatePassword(string password){

                string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$";

                return Regex.IsMatch(password ,pattern );
        }

        // Book Appointment function
        static void BookAppoinment(Patient patient)
        {
            Console.WriteLine("Please, select a doctor: ");

            // Displaying list of initialized doctors here for the user to choose from them
            for(int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine($"{i+1}. {doctors[i].Name}");
            }

            int choice = Int32.Parse(Console.ReadLine());
            // Validating choice for doctor here
            if (choice < 1 || choice > doctors.Count)
            {       
                Console.WriteLine("Invalid choice");
                return;
            }
            
            Doctor doctor = doctors[choice -1];
            //-------------------------------

            Console.WriteLine("Please, select a location: ");

            // Dsplaying list of initialized locations like above for doctors
            for (int i = 0; i < locations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {locations[i].Name}");
            } 
            choice = Int32.Parse(Console.ReadLine());

            // Validate choice for the location too
            if (choice < 1 || choice > locations.Count)
            {
                Console.WriteLine("Invalid choice");
                return;
            }
            Location location = locations[choice -1];
            //-------------------------------------------------

            Console.WriteLine("Please, select a date and time: ");
            
            // Display list of available dates and times
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"{i}. {DateTime.Now.AddDays(i).ToString("yyyy-MM-dd")} 10:30 AM");
            }

            choice = Int32.Parse(Console.ReadLine());

            // Validate choice again for this choice
            if (choice < 1 || choice > 10)
            {
                Console.WriteLine("Invalid choice");
                return;
            }

            DateTime dateTime = DateTime.Now.AddDays(choice);

            Console.WriteLine("Please, enter your illness category:");
            Console.WriteLine("1. General Check Up");
            Console.WriteLine("2. Dermatology");
            Console.WriteLine("3. Orthopedics");
            Console.WriteLine("4. Cardiology");

            choice = Int32.Parse(Console.ReadLine());
            IllnessCategory illness;
            
            switch(choice){
                    case 1 : 
                            illness = IllnessCategory.General_Check_Up;
                        break;
                    case 2 : 
                        illness = IllnessCategory.Dermatology;
                        break;
                    case 3 : 
                           illness = IllnessCategory.Orthopedics;
                        break;
                    case 4 : 
                         illness = IllnessCategory.Cardiology;
                        break;
                    default : 
                            illness = IllnessCategory.General_Check_Up;
                            break;
            }
            Inquiry inquiry = SubmitInquiry(patient);

            Appointment appointment = new Appointment(patient, doctor, dateTime, inquiry, illness);

            appointments.Add(appointment);

            Console.WriteLine("Appointment booked successfully");
        } 

        // Create and Submit Inquiry
        static Inquiry SubmitInquiry(Patient patient)
        {
            Console.Write("Please, enter your address: ");
            string address = Console.ReadLine();

            Console.Write("Please, enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Please, enter your emergency phone number: ");
            string emergencyphonenumber = Console.ReadLine();

            Console.Write("Please, enter your symptoms: ");
            string symptoms = Console.ReadLine();

            Console.Write("Please, enter duration of the your symptoms: ");
            string durationofsymptoms = Console.ReadLine();

            Console.Write("Please, enter your previous medical history: ");
            string previousmedicalhistory = Console.ReadLine();

            Console.Write("Please, enter your current medications: ");
            string currentmedications = Console.ReadLine();

            Console.Write("Please, enter your allergies: ");
            string allergies = Console.ReadLine();

            Console.Write("Please, enter additional comments: ");
            string additionalcomments = Console.ReadLine();

            Console.Write("Please, enter your reason for booking an appoinment ");
            string patientexplanation = Console.ReadLine();

            Console.Write("Please, enter your BC healthcard expiry date (YYYY-MM-DD): ");
            DateTime bcexpire = DateTime.Now;    

            while (true) 
            {
                try
                {
                    string bchealthcardexpirydate = Console.ReadLine();
                    bcexpire = DateTime.Parse(bchealthcardexpirydate);
                    break;          
                }
                catch (System.Exception)
                {
                    Console.Write("Please, use the right format (YYYY-MM-DD): ");
                }
            }

            Inquiry inquiry = new Inquiry(
                patient.Name,
                patient.DOB,
                address,
                email,
                patient.PhoneNumber,
                emergencyphonenumber,
                symptoms,
                durationofsymptoms,
                previousmedicalhistory,
                currentmedications,
                allergies,
                additionalcomments,
                patientexplanation,
                bcexpire
            );

            return inquiry;
        }

        // Patient Update Profile
        static void UpdateProfile(Patient patient)
        {
            Console.Write("Please, enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Enter date of birth (YYYY-MM-DD): ");
            string dob = Console.ReadLine();
            
            patient.Name = name;
            patient.DOB = DateTime.Parse(dob);

            Console.WriteLine("Profile updated successfully");
        }

        static void DoctorMenu(Doctor doctor)
        {
            while(true)
            {
                // Doctor menu options 
                Console.WriteLine("1. View Appointments");
                Console.WriteLine("2. Update Profile");
                Console.WriteLine("0. Logout");

                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ViewAppointments(doctor);
                        break;

                    case 2:
                        UpdateDoctorProfile(doctor);
                        break;

                     case 0:
                        Console.WriteLine("Logging out...");
                        return;
                    
                    default:
                    Console.WriteLine("Invalid choice");
                    continue;
                }
            }
        }


        // Doctor Update Profile
        static void UpdateDoctorProfile(Doctor doctor)
        {
            Console.Write("Please, enter your name: ");
            string name = Console.ReadLine();

            doctor.Name = name;

            Console.WriteLine("Profile updated successfully");
        }

        // View Appointments
        static void ViewAppointments(Doctor doctor)
        {
            Console.WriteLine("Select an appointment ID");

            // Display list of appointments
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].Doctor == doctor)
                    Console.WriteLine($"ID: {i + 1}. {appointments[i].Patient.Name} - {appointments[i].DateTime.ToString("yyyy-MM-dd")}");
            }

            int choice = Int32.Parse(Console.ReadLine());

            // validate the user's choice again
            if (choice < 1 || choice > appointments.Count) 
            {
                Console.WriteLine("Invalid choice");
                return;
            }

            Appointment appointment = appointments[choice - 1];

            Console.WriteLine($"Patient: {appointment.Patient.Name}");
            Console.WriteLine($"Date: {appointment.DateTime.ToString("yyyy-MM-dd")}");
            Console.WriteLine($"Location: {locations[0].Name}");

            Console.WriteLine("1. Review Patient's Inquiry");
            Console.WriteLine("2. Update Appoitment Result");
            choice = Int32.Parse(Console.ReadLine());

            switch(choice){
            case 1:
            
                Console.WriteLine($"Date of Birth: {appointment.Inquiry.DateOfBirth}");
                Console.WriteLine($"Address: {appointment.Inquiry.Address}");
                Console.WriteLine($"Email: {appointment.Inquiry.Email}");
                Console.WriteLine($"Phone Number: {appointment.Inquiry.PhoneNumber}");
                Console.WriteLine($"Emergency Phone Number: {appointment.Inquiry.EmergencyPhoneNumber}");
                Console.WriteLine($"Symptoms: {appointment.Inquiry.Symptoms}");
                Console.WriteLine($"Duration of Symptoms: {appointment.Inquiry.DurationOfSymptoms}");
                Console.WriteLine($"Previous Medical History: {appointment.Inquiry.PreviousMedicalHistory}");
                Console.WriteLine($"Current Medications: {appointment.Inquiry.CurrentMedications}");
                Console.WriteLine($"Allergies: {appointment.Inquiry.Allergies}");
                Console.WriteLine($"Allergies: {appointment.Inquiry.Allergies}");
                Console.WriteLine($"Allergies: {appointment.Inquiry.Allergies}");
                Console.WriteLine($"Additonal Comments: {appointment.Inquiry.AdditonalComments}");
                Console.WriteLine($"Reason for Booking an Appointment: {appointment.Inquiry.PatientExplanation}");
                Console.WriteLine($"BC Healthcard Expiry Date: {appointment.Inquiry.BCHealthcardExpiryDate}");
                break;

            case 2: 
                Console.Write("Please enter the details about appointment result: ");
                string details = Console.ReadLine();
                Result result = new Result(details);
                appointment.Result = result;
                break;
            default: 
                Console.Write("the input is mismatch !"); 
                break;    
            }
            

        }

        // Staff menu
        static void StaffMenu(Staff staff)
        {
            while(true)
            {
                // Staff menu options
                Console.WriteLine("1. Update Profile");
                Console.WriteLine("0. Logout");

                int choice = Int32.Parse(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        StaffUpdateProfile(staff);
                        break;
                
                    case 0:
                        Console.WriteLine("Logging out...");
                        return;
                    
                    default:
                        Console.WriteLine("Invalid choice");
                        continue;
                }
            }
        }
        static void StaffUpdateProfile(Staff staff)
        {
            Console.Write("Enter your name: (Staff) ");
            string name = Console.ReadLine();

            staff.Name = name;

            Console.WriteLine("Profile updated successfully");
        }

    }
} 
    
        
