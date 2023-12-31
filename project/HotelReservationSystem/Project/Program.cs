using System;
//Temel işlevleri sağlayan bir kütüphanedir.
using System.Collections.Generic;
//Temel koleksiyon sınıflarını içerir (List, Dictionary, vb.).
using System.Diagnostics;
//Uygulamalarınızın çalışma süreci ve performansıyla ilgili bilgileri toplamanıza ve işlemenize olanak tanır.


class Hotel
{
    // odalar adında oda nesne listesini get,set ile okuma ve yazma işlemi ayarlanıyor.
    public string Name { get; set; }
    public List<Room> Rooms { get; set; }

    // Otel sınıfının yapıcı metodu
    public Hotel(string Room)
    {
        //Odaların listesi oluşturulup oda çeşit nesneleri ekleniyor.

        Rooms = new List<Room>();
        Rooms.Add(new normalRoom(1));
        Rooms.Add(new largeNormalRoom(2));
        Rooms.Add(new premiumRoom(3));
        Rooms.Add(new largePremiumRoom(4));
        Rooms.Add(new kingSuite(5));
        Rooms.Add(new largeKingSuite(6));

    }

    // Odayı otel listesine ekleyen metot
    public void addRoom(Room room)
    {
        Rooms.Add(room);
    }
}

abstract class Room
{
    //abstract(soyut) oda sınıfı içinde kullanılacak olan sınıfları get,set ile okuma ve yazma işlemi ayarlanıyor.
    public int roomNumber { get; set; }
    public string customerName { get; set; }
    public string customerSurname { get; set; }
    public int numberOfDaysToStay { get; set; }
    public int numberOfPeople { get; set; }
    public int roomPersonCapacity { get; set; }



    // Oda sınıfının yapıcı metodu
    public Room(int RoomNumber, int RoomPersonCapacity)
    {
        //Gerekli olan bilgiler tanımlanıyor.
        roomNumber = RoomNumber;
        customerName = "";
        customerSurname = "";
        numberOfDaysToStay = 0;
        numberOfPeople = 0;
        roomPersonCapacity = RoomPersonCapacity;

    }
    // Oda fiyatını hesaplamak için soyut metot
    public abstract decimal calculatePrice();
    // decimal burda ikili bir tam sayı değerini tutuyor.

}

class normalRoom : Room
//NormalOda adında alt sınıf oluşturuldu.
{
    // NormalOda sınıfının yapıcı metodu
    public normalRoom(int roomNumber) : base(roomNumber, 1) { }

    // Oda fiyatını hesaplamak için metot
    public override decimal calculatePrice()
    {
        return numberOfDaysToStay * 100;
    }
}
// Diğer alt sınıflar için de benzer değerlendirmeler yapılabilir.


//NormalOdaGenis adında alt sınıf oluşturuldu.
class largeNormalRoom : Room

{
    public largeNormalRoom(int roomNumber) : base(roomNumber, 2) { }

    public override decimal calculatePrice()
    {
        return numberOfDaysToStay * 150;
    }
}

//PremiumOda adında alt sınıf oluşturuldu.
class premiumRoom : Room

{
    public premiumRoom(int roomNumber) : base(roomNumber, 3) { }

    public override decimal calculatePrice()
    {
        return numberOfDaysToStay * 200;
    }
}

//PremiumOdaGenis adında alt sınıf oluşturuldu.
class largePremiumRoom : Room
//PremiumOdaGenis adında alt sınıf oluşturuldu.
{
    public largePremiumRoom(int roomNumber) : base(roomNumber, 4) { }

    public override decimal calculatePrice()
    {
        return numberOfDaysToStay * 250;
    }
}

//KralDairesi adında alt sınıf oluşturuldu.
class kingSuite : Room

{
    public kingSuite(int roomNumber) : base(roomNumber, 5) { }

    public override decimal calculatePrice()
    {
        return numberOfDaysToStay * 250;
    }
}

//KralDairesiGenis adında alt sınıf oluşturuldu.
class largeKingSuite : Room

{
    public largeKingSuite(int roomNumber) : base(roomNumber, 6) { }

    public override decimal calculatePrice()
    {
        return numberOfDaysToStay * 300;
    }
}



class Program
{
    static Dictionary<string, string> userCredentials = new Dictionary<string, string>
    {
        {"gurkan", "123"},
        {"kutluk", "456"}
        
    };
    //userCredentials adlı bir sözlük kullanıcı adlarına karşılık gelen şifreleri içerir.

    static void Main()
    {
        Console.WriteLine("Welcome to the Hotel Reservation System");
        Console.Write("Username: ");
        string username = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (AuthenticateUser(username, password))
        {
            Console.WriteLine("Login successful!");
            StartHotelReservationSystem();
        }
        else
        {
            Console.WriteLine("Invalid username or password. Exiting...");
        }
    }

    static bool AuthenticateUser(string username, string password)
    //AuthenticateUser fonksiyonu kullanıcının doğruluğunu kontrol eder.
    {
        if (userCredentials.ContainsKey(username) && userCredentials[username] == password)
        {
            return true;
        }
        return false;
    }

    static void StartHotelReservationSystem()
    {
        Hotel hotel = new Hotel("Lüx Hotel");
        
        while (true)
        {
            Console.WriteLine("\n--- Hotel Reservation System ---");
            Console.WriteLine("1. View Hotel Status");
            Console.WriteLine("2. Make a Reservation");
            Console.WriteLine("3. Delete Reservation");
            Console.WriteLine("4. View Total Amount");
            Console.WriteLine("5. Exit");
            //Konsolda küçük bir menü yapıldı.

            Console.WriteLine("What is your choise?: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    viewHotelStatus(hotel);
                    break;
                case "2":
                    makeAReservation(hotel);
                    break;
                case "3":
                    deleteReservation(hotel);
                    break;
                case "4":
                    viewTotalAmount(hotel);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
            //Switch komutu ile kullanıcının seçtiği seçeneğe göre case komutu kullanılarak istenen şeyi kullanıcıya gösterildi.
        }
    }
    static void viewHotelStatus(Hotel hotel)
    {
        Console.WriteLine($"{hotel.Name} Hotel Status:");

        foreach (Room room in hotel.Rooms)
        //bütün odaları tek tek geziyor.
        {
            Console.WriteLine($"Room {room.roomNumber}: {(room.customerName == "" ? "Empty" : $"{room.customerName} {room.customerSurname}, {room.numberOfDaysToStay} day, {room.numberOfPeople} people")}");
            // odaların bütün bilgileri kullanıcıya verir.
        }
    }
    //kullanıcıya otelde bulunan rezervasyonları gösterdi.


    static void makeAReservation(Hotel hotel)
    {
        Console.Write("Customer name: ");
        string Name = Console.ReadLine();
        //Kullanıcan Müşterinin adı istendi

        Console.Write("Customer Surname: ");
        string Surname = Console.ReadLine();
        //Kullanıcıdan Müşterinin Soyismi istendi.

        Console.Write("How many people?: ");
        int numberofpeople;
        //Kullanıcıdan Kaç kişi olcağını istendi.
        while (!int.TryParse(Console.ReadLine(), out numberofpeople) || numberofpeople <= 0)
        {
            Console.WriteLine("Invalid number of people. Please enter a positive integer.");
            Console.Write("How many people?: ");
        }
        //While döngüsü burda girilecek olan kişi sayısının pozitif bir tam sayı olması için sürekli kullanıcıya pozitif bir tam sayı girmesini ister.

        Console.Write("Which room ? (1-Normal (for 1 people), 2-Normal Large (for 2 people), 3-Premium (for 3 people), 4-Premium Large (for 4 people), 5-King Suite (for 5 people), 6-King Suite Large ( for 6 people)): "); //Kullanıcıya hangi odayı istediğini sorduk.
        int RoomNumber;
        while (!int.TryParse(Console.ReadLine(), out RoomNumber) || RoomNumber < 1 || RoomNumber > 6)
        {
            Console.WriteLine("Invalid room number. Please enter again.");
            Console.Write("Which Room (1-Normal (for 1 people), 2-Normal Large (for 2 people), 3-Premium (for 3 people), 4-Premium Large (for 4 people), 5-King Suite (for 6 people), 6-King Suite Large(for 6 people): ");
        }
        //While döngüsü burda 1 ila 6 arası hariç girilen sayıların geçersiz olduğunu kullanıya söyler ve istenen oda numarasını tekrar sorar.

        Room targetRoom = hotel.Rooms.Find(room => room.roomNumber == RoomNumber);
        //Hedef oda Find komutu ile bulunur.

        if (targetRoom != null && targetRoom.customerName == "" && numberofpeople <= targetRoom.roomPersonCapacity)
        // if komutu ile hedef odanın boş olup olmadığını,hedef odada başka birinin olup olmadığını ve hedef odanın kalacak kişi sayısını büyük olup olmadığını kontol ettik.
        {
            Console.Write("Number of Days to Stay: ");
            //kullanıcıya kalacak gün sayısı soruldu.
            int numberOfDaysToStay;
            while (!int.TryParse(Console.ReadLine(), out numberOfDaysToStay) || numberOfDaysToStay <= 0)
            {
                Console.WriteLine("Invalid number of days. Please enter a positive integer.");
                Console.Write("Number of Days to Stay: ");
            }
            //while döngüsü ile kalacak gün sayisinin pozitif olana kadar kullanıcıya sorulması sağlandı.
            //TryPaste ile string bir veriyi yukarıda kullanıldığı gibi int bir veriye çevrilir.

            targetRoom.customerName = Name;
            targetRoom.customerSurname = Surname;
            targetRoom.numberOfDaysToStay = numberOfDaysToStay;
            targetRoom.numberOfPeople = numberofpeople;
            //Girilecek olan rezervasyonun bilgileri ilgili bölüme yazıldı.

            Console.WriteLine($"{Name} {Surname}, room no:{RoomNumber} , For {numberofpeople} person , Made a reservation for the {numberOfDaysToStay} day.");
            //kullanıya rezervasyonun bütün bilgileri konsolda verildi.
        }
        else if (targetRoom != null && targetRoom.customerName == "" && numberofpeople > targetRoom.roomPersonCapacity)
        {
            Console.WriteLine($"The room you choose for {targetRoom.roomPersonCapacity} people. Please choose a larger room.");
        }
        //Eğer oda kapasitesinden büyük kişi sayısı girilirse kullanıya uyarı verildi.
        else
        {
            Console.WriteLine($"Room {RoomNumber} is full or invalid or has too many people. ");
        }
        //Hali hazırda dolu olan bir odaya rezervasyon yapılırsa kullanıcıya uyarı verildi.
    }

    static void deleteReservation(Hotel hotel)
    {
        //Kullanıcıya hangi oda ilgili işlem yapıcağı soruldu.
        Console.Write("Which room ? (1-Normal , 2-Normal Large , 3-Premium , 4-Premium Large , 5-King Suite , 6-King Suite Large ): ");

        int roomNumber;
        //TryParse komutu kullanıcının tam sayı girmesini ve tam sayının 1 ile 6 arasında olduğunu kontrol eder.
        while (!int.TryParse(Console.ReadLine(), out roomNumber) || roomNumber < 1 || roomNumber > 6)
        {
            Console.WriteLine("Invalid room number. Please enter again.");
            Console.Write("Which room ? (1-Normal , 2-Normal Large , 3-Premium , 4-Premium Large , 5-King Suite , 6-King Suite Large): ");
        }
        //while döngüsü kullanıcı doğru oda numarası girene kadar tekrardan sormaya devam eder.

        //Hedef oda Find komutu ile bulunur.
        Room targetRoom = hotel.Rooms.Find(room => room.roomNumber == roomNumber);

        // eğer hedef oda boş değilse ve hedef odada misafir adı var ise,ilgili oda için rezervasyonu tamamen siler.
        if (targetRoom != null && targetRoom.customerName != "")
        {
            Console.WriteLine($"Reservation for room {roomNumber} was deleted. {targetRoom.customerName} {targetRoom.customerSurname}, {targetRoom.numberOfDaysToStay} day, {targetRoom.numberOfPeople} person");
            targetRoom.customerName = "";
            targetRoom.customerSurname = "";
            targetRoom.numberOfDaysToStay = 0;
            targetRoom.numberOfPeople = 0;
        }
        //eğer hedef oda boş işe kullanıcıya bilgi verilir.
        else
        {
            Console.WriteLine($"{roomNumber}.room is already empty.");
        }

    }

    static void viewTotalAmount(Hotel hotel)
    {
        decimal totalAmount = 0;
        decimal customerPrice = 0;

        //foreach ile odaları gezerek dolu odaların ne kadar fiyat ödeceğini gösterdik ve en sonunda toplam ödenecek fiyat kullanıcıya gösterildi.
        foreach (Room room in hotel.Rooms)
        {
            if (room.customerName != "")
            {
                customerPrice = room.calculatePrice();
                totalAmount += customerPrice;


                Console.WriteLine($"{room.roomNumber}. Room: {room.customerName} {room.customerSurname}, {room.numberOfDaysToStay} day, for {room.numberOfPeople} people {customerPrice} $");
            }
            


        }

        Console.WriteLine($"\nTotal Amount: {totalAmount} $");
    }
}