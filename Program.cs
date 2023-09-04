using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace HelathCare53
{
    class Program
    {
        // List to store patient profiles
        static List<Patient> patients = new List<Patient>();

        // List to store doctor profiles
        static List<Doctor> doctors = new List<Doctor>();

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

            public Patient(string name, string dob, string healthCareNum)
            {
                Name = name;
                DOB = DateTime.Parse(dob);
                HealthcareNumber = healthCareNum;
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

        class Appointment
        {
            public Patient Patient {get; set;}
            public Doctor Doctor {get; set;}
            public DateTime DateTime {get; set;}

            public Appointment(Patient patient, Doctor doctor, DateTime dateTime)
            {
                Patient = patient;
                Doctor = doctor;
                DateTime = dateTime;
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
            DoctorMenu();
        }
        static void StaffLogin()
        {
            StaffMenu();
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
            Appointment appointment1 = new Appointment(omid, Ali, new DateTime(2023, 9, 10, 10, 30, 0));
            Appointment appointment2 = new Appointment(lenore, Arta, new DateTime(2023, 10, 10, 16, 0, 0));

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
            string password = Console.ReadLine();

            patient.Password = password;

            patients.Add(patient);

            Console.WriteLine("Patient profile created successfully");
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
            
            // Dsplay list of available dates and times
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

            Appointment appointment = new Appointment(patient, doctor, dateTime);

            appointments.Add(appointment);

            Console.WriteLine("Appointment booked successfully");
        } 

        // Submit Inquiry
        static void SubmitInquiry(Patient patient)
        {
            Console.Write("Please, enter Inquiry: ");
            string inquiry = Console.ReadLine();

            Console.WriteLine("Inquiry submitted successfully");
        }

        // update Profile
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
                        UpdateProfile();
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
                Console.WriteLine("1. View Appointments");
                Console.WriteLine("2. Update Profile");
                Console.WriteLine("0. Logout");

                int choice = Int32.Parse(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        ViewAppointments();
                        break;
                    
                    case 2:
                        UpdateProfile();
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
    }
} 
    
        
