using System;
using System.Collections.Generic;

abstract class Person
{
    protected int NIM;
    protected string Nama;

    public Person(int nim, string nama)
    {
        NIM = nim;
        Nama = nama;
    }
    public abstract string GetDetails();
    public int GetNIM() => NIM;
    public void SetNama(string nama) => Nama = nama;
    public string GetNama() => Nama;

    class Mahasiswa : Person
    {
        private string Jurusan;

        public Mahasiswa(int nim, string nama, string jurusan) : base(nim, nama)
        {
            Jurusan = jurusan;
        }

        public override string GetDetails()
        {
            return $"NIM : {NIM}, Nama : {Nama}, Jurusan : {Jurusan}";
        }

        public void SetJurusan(string jurusan) => Jurusan = jurusan;
        public string GetJurusan() => Jurusan;
    }

    class MenuMahasiswa
    {
        private List<Mahasiswa> mahasiswaList = new List<Mahasiswa>();
        public void Create(Mahasiswa mahasiswa)
        {
            if (IsNimExists(mahasiswa.GetNIM()))
            {
                Console.WriteLine("NIM sudah terdaftar dalam list, Silahkan masukkan NIM lain");
                return;
            }
            else
                mahasiswaList.Add(mahasiswa);
            Console.WriteLine($"Data mahasiswa berhasil ditambahkan.");
        }
        public void Read()
        {
            if (mahasiswaList.Count == 0)
            {
                Console.WriteLine("Belum ada data mahasiswa.");
                return;
            }

            Console.WriteLine("Daftar Mahasiswa:");
            foreach (var mahasiswa in mahasiswaList)
            {
                Console.WriteLine(mahasiswa.GetDetails());
            }
        }
        public void Update(int nim)
        {
            Mahasiswa mahasiswa = mahasiswaList.Find(m => m.GetNIM() == nim);
            if (mahasiswa == null)
            {
                Console.WriteLine("NIM tidak ditemukan.");
                return;
            }

            Console.Write("Masukkan nama : ");
            string namaBaru = Console.ReadLine();
            Console.Write("Masukkan jurusan : ");
            string jurusanBaru = Console.ReadLine();

            mahasiswa.SetNama(namaBaru);
            mahasiswa.SetJurusan(jurusanBaru);
            Console.WriteLine($"Data dengan NIM {nim} berhasil diperbarui.");
        }

        public void Delete(int nim)
        {
            Mahasiswa mahasiswa = mahasiswaList.Find(NIM => NIM.GetNIM() == nim);
            if (mahasiswa == null)
            {
                Console.WriteLine("NIM tidak ditemukan.");
                return;
            }

            mahasiswaList.Remove(mahasiswa);
            Console.WriteLine($"Data dengan NIM {nim} berhasil dihapus.");
        }

        public bool IsNimExists(int nim)
        {
            return mahasiswaList.Exists(NIM => NIM.GetNIM() == nim);
        }
    }

    class Program
    {
        static void Main()
        {
            MenuMahasiswa menu = new MenuMahasiswa();
            int pilihan;
            do
            {
                Console.WriteLine("\n ---- Menu Management Mahasiswa ----");
                Console.WriteLine("1. Tambah Mahasiswa");
                Console.WriteLine("2. Lihat Mahasiswa");
                Console.WriteLine("3. Update Mahasiswa");
                Console.WriteLine("4. Hapus Mahasiswa");
                Console.WriteLine("5. Keluar");
                Console.Write("Pilih menu (1-5): ");
                if (!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("Input hanya berupa angka!");
                    continue;
                }

                switch (pilihan)
                {
                    case 1:
                        Console.Write("Masukkan NIM: ");
                        if (!int.TryParse(Console.ReadLine(), out int nimTambah))
                        {
                            Console.WriteLine("Input NIM tidak valid!");
                            break;
                        }

                        Console.Write("Masukkan Nama: ");
                        string nama = Console.ReadLine();
                        Console.Write("Masukkan Jurusan: ");
                        string jurusan = Console.ReadLine();
                        Mahasiswa mahasiswaBaru = new Mahasiswa(nimTambah, nama, jurusan);
                        menu.Create(mahasiswaBaru);
                        break;

                    case 2:
                        menu.Read();
                        break;

                    case 3:
                        Console.Write("Masukkan NIM mahasiswa yang ingin diupdate : ");
                        if (!int.TryParse(Console.ReadLine(), out int nimUpdate))
                        {
                            Console.WriteLine("Input NIM tidak valid!");
                            break;
                        }
                        menu.Update(nimUpdate);
                        break;

                    case 4:
                        Console.Write("Masukkan NIM mahasiswa yang ingin dihapus : ");
                        if (!int.TryParse(Console.ReadLine(), out int nimHapus))
                        {
                            Console.WriteLine("Input NIM tidak valid!");
                            break;
                        }
                        menu.Delete(nimHapus);
                        break;

                    case 5:
                        Console.WriteLine("Kamu memilih keluar.");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            } while (pilihan != 5);
        }
    }
}
