using System;
using System.Text.RegularExpressions;
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
            public string Password {get; set;} = "";
            public string PhoneNumber{get ; set } = ""


            public Patient(string name, string dob, string healthCareNum , string phoneNumber)
            {
                Name = name;
                DOB = DateTime.Parse(dob);
                HealthcareNumber = healthCareNum;
                PhoneNumber = phoneNumber
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

        class Appointment
        {
            enum Illness{
                General_Check_Up ,
                Dermatology , 

                Orthopedics , 

                Cardiology 




            }
            public Patient Patient {get; set;}
            public Doctor Doctor {get; set;}
            public DateTime DateTime {get; set;}
            public Inquiry Inquiry {get; set;}
            public Illness illness{get ; set}

            public Appointment(Patient patient, Doctor doctor, DateTime dateTime, Inquiry inquiry)
            {
                Patient = patient;
                Doctor = doctor;
                DateTime = dateTime;
                Inquiry = inquiry;
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
                string FullName, DateTime DateOfBirth, string Address,
                string Email, string PhoneNumber, string EmergencyPhoneNumber,
                string Symptoms, string DurationOfSymptoms, string PreviousMedicalHistory,
                string CurrentMedications, string Allergies, string AdditonalComments,
                string PatientExplanation, DateTime BCHealthcardExpiryDate
            )
            {
                FullName = FullName;
                DateOfBirth = DateOfBirth;
                Address = Address;
                Email = Email;
                PhoneNumber = PhoneNumber;
                EmergencyPhoneNumber = EmergencyPhoneNumber;
                Symptoms = Symptoms;
                DurationOfSymptoms = DurationOfSymptoms;
                PreviousMedicalHistory = PreviousMedicalHistory;
                CurrentMedications = CurrentMedications;
                Allergies = Allergies;
                AdditonalComments = AdditonalComments;
                PatientExplanation = PatientExplanation;
                BCHealthcardExpiryDate = BCHealthcardExpiryDate;
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
    

        static void Main(string[] args)
        {
            // Initialize data
            Initialize();

            while(true)
            {
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
                    Console.WriteLine("Invalid password");
                
                    AskRecoveryOption();
                    return;
                }
                PatientMenu(patient);
                // New patient
            }
            else
            {
                Console.WriteLine("Creating new patient profile...");
                CreatePatientProfile(healthCareNum);
            }
        }

        static void AskRecoveryOption(){
            Console.Write("Do you want to send the recover password to Your phone number?");
            Console.Write("1. Yes");
            Console.Write("2. No");
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
                Console.WriteLine("2. Submit Inquiry");
                Console.WriteLine("3. Update Profile");
                Console.WriteLine("0. Logout");

                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        BookAppoinment(patient);
                        break;
                    
                    case 2:
                        SubmitInquiry(patient);
                        break;
                    
                    case 3:
                        UpdateProfile(patient);
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

        static void DoctorLogin()
        {
            // Doctor login logic here by calling its own external function within this function
            Console.Write("Please, enter your ID: ");
            string id = Console.ReadLine();

            // Exiting doctor
            if (IsExistingDoctor(id))
            {
                Doctor doctor = GetDoctor(id);
                DoctorMenu();
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
                StaffMenu();
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
        static Doctor GetStaff(string id)  // Get staff object
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
            Patient omid = new Patient("Omid Najjar", "1992-09-25", "BC09151234");
            omid.Password = "OmidNjr4!@#";

            Patient lenore = new Patient("lenore Najjar", "2013-07-01", "BC09155678");
            lenore.Password = "LenoreNjr#";
            
            // Add them to patients List
            patients.Add(omid);
            patients.Add(lenore);

            // Create sample doctors below
            Doctor Ali = new Doctor("Dr. Ali Najjar", "D0915111");
            Doctor Arta = new Doctor("Dr. Eli Najjar", "09150090");

            // Add doctors to Doctor's List
            doctors.Add(Ali);
            doctors.Add(Arta);

            // Create sample appointments
            Appointment appointment1 = new Appointment(omid, Ali, new DateTime(2023, 9, 10, 10, 30, 0), null);
            Appointment appointment2 = new Appointment(lenore, Arta, new DateTime(2023, 10, 10, 16, 0, 0), null);

            // Add to appointments List
            appointments.Add(appointment1);
            appointments.Add(appointment2);  

            // Create sample locations
            Location location1 = new Location("General Hospital", "555 Willingdon Ave, Burnaby BC", "604-111-1234");
            Location location2 = new Location("Surrey health Hospital", "666 Somewher St, Surrey BC", "604-222-5678");

            // and add locations to the List
            locations.Add(location1);
            locations.Add(location2);
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
            Console.Write("Please, enter your name (patient): ");
            string name = Console.ReadLine();

            Console.Write("Please, enter your date of birth: ");
            string dob = Console.ReadLine();

            Patient patient = new Patient(name, dob, healthCareNum);

            Console.Write("Please enter password: ");
            while(true)
            {
                string password = Console.ReadLine();

                if(ValidatePassword(password)){
                    break;
                }
                else 
                {
                    Console.Write("your password must included combination of lower case and upper case and number and special characters, also it must be at least 8 charachter and maximum 16 character")
                }

            }

            patient.Password = password;

            while (true)
            {
                Console.Write("Please enter your recover phone number:");
                string phoneNumber = Console.ReadLine();
                
                if(ValidatePhoneNumber(phoneNumber)){
                    break;
                }
                else {
                    Console.Write("your input as phone number is invalid");
                }
            }
            patient.PhoneNumber = phoneNumber;
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
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i + 1}. {DateTime.Now.AddDays(i).ToString("yyyy-MM-dd")} 10:30 AM");
                Console.WriteLine($"{i + 2}. {DateTime.Now.AddDays(i).ToString("yyyy-MM-dd")} 2:00 PM");
            }

            choice = Int32.Parse(Console.ReadLine());

            // Validate choice again for this choice
            if (choice < 1 || choice > 10)
            {
                Console.WriteLine("Invalid choice");
                return;
            }

            DateTime dateTime = DateTime.Now.AddDays(choice -1);

            Inquiry inquiry = SubmitInquiry(patient);

            Appointment appointment = new Appointment(patient, doctor, dateTime, inquiry);

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
            while (true) 
            {
                try
                {
                    string bchealthcardexpirydate = Console.ReadLine();
                    DateTime bcexpire = DateTime.Parse(bchealthcardexpirydate);                  
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

        static void DoctorMenu()
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
                        ViewAppointments();
                        break;

                    case 2:
                        UpdateDoctorProfile();
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
        static void ViewAppointments()
        {
            Console.WriteLine("Select an appointment");

            // Display list of appointments
            for (int i = 0; i < appointments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {appointments[i].Patient.Name} - {appointments[i].DateTime.ToString("yyyy-MM-dd")} {appointments[i].DateTime.ToString("hh:mm:tt")}");
            }

            int choice = Int32.Parse(Console.ReadLine());

            // validate the user's choice again
            if (choice < 1 || choice > appointments.Count) 
            {
                Console.WriteLine("Invalid choice");
                return;
            }

            Appointment appointment = appointments[choice -1];

            Console.WriteLine($"Patient: {appointment.Patient.Name}");
            Console.WriteLine($"Date: {appointment.DateTime.ToString("yyyy-MM-dd")}");
            Console.WriteLine($"Time: {appointment.DateTime.ToString("hh:mm:tt")}");
            Console.WriteLine($"Location: {locations[0].Name}");

            Console.WriteLine("For review patient's inquiry enter 1: ");
            choice = Int32.Parse(Console.ReadLine());

            if (choice == 1) {
                Console.WriteLine($"Date of Birth: {appointment.Inquiry.DateOfBirth}");
                Console.WriteLine($"Address: {appointment.Inquiry.Address}");
                Console.WriteLine($"Email: {appointment.Inquiry.Email}");
                Console.WriteLine($"Phone Number: {appointment.Inquiry.PhoneNummber}");
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
            }

        }

        static void UpdateProfile() // //////////////////
        {
            Console.Write("Enter your name: (doctor) ");
            string name = Console.ReadLine();

            Console.Write("Enter doctor ID: ");
            string id = Console.ReadLine();

            Doctor doctor = new Doctor(name, id);

            doctors.Add(doctor);

            Console.WriteLine("Profile updated successfully");
        }

        // Staff menu
        static void StaffMenu()
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
                        StaffUpdateProfile();
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
        static void StaffUpdateProfile()
        {
            Console.Write("Enter your name: (Staff) ");
            string name = Console.ReadLine();

            Console.Write("Enter Staff ID: ");
            string id = Console.ReadLine();

            Staff staff = new Staff(name, id);

            staffs.Add(staff);

            Console.WriteLine("Profile updated successfully");
        }

    }
} 
    
        
