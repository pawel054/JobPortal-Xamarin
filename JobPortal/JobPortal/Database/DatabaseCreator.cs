﻿using JobPortal.Class;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JobPortal.Database
{
    public class DatabaseCreator
    {
        private static readonly string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "JobPortalDatabasetest.db");
        public static void CreateDb()
        {

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string tableCommand =
                    @"CREATE TABLE IF NOT EXISTS firma (firma_id INTEGER PRIMARY KEY AUTOINCREMENT, nazwa TEXT, adres TEXT, informacja TEXT);" +
                    "CREATE TABLE IF NOT EXISTS kategoria (kategoria_id INTEGER PRIMARY KEY AUTOINCREMENT, nazwa TEXT);" +
                    "CREATE TABLE IF NOT EXISTS uzytkownik (user_id INTEGER PRIMARY KEY AUTOINCREMENT, email TEXT, haslo TEXT, isadmin BOOLEAN);" +
                    "CREATE TABLE IF NOT EXISTS oferta (oferta_id INTEGER PRIMARY KEY AUTOINCREMENT, firma_id INTEGER, kategoria_id INTEGER, nazwa_stanowiska TEXT, poziom_stanowiska TEXT, rodzaj_umowy TEXT, rodzaj_pracy TEXT, wymiar_zatrudnienia TEXT, wynagrodzenie TEXT, dni_pracy TEXT, godziny_pracy TEXT, data_waznosci DATE, obraz_src TEXT, FOREIGN KEY (firma_id) REFERENCES firma(firma_id) ON UPDATE CASCADE ON DELETE CASCADE, FOREIGN KEY (kategoria_id) REFERENCES kategoria(kategoria_id) ON UPDATE CASCADE ON DELETE CASCADE); " +
                    "CREATE TABLE IF NOT EXISTS profil (profil_id INTEGER PRIMARY KEY AUTOINCREMENT, user_id INTEGER, imie TEXT, nazwisko TEXT, data_urodzenia DATE, numer_telefonu TEXT, zdjecie_profilowe TEXT, adres_zamieszkania TEXT, aktualne_stanowisko TEXT, aktualne_stanowisko_opis TEXT, podsumowanie_zawodowe TEXT, FOREIGN KEY (user_id) REFERENCES uzytkownik(user_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS uzytkownik_aplikacje (aplikacja_id INTEGER PRIMARY KEY AUTOINCREMENT, user_id INTEGER, oferta_id INTEGER, status TEXT, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE NO ACTION, FOREIGN KEY (user_id) REFERENCES uzytkownik(user_id) ON UPDATE CASCADE ON DELETE NO ACTION);" +
                    "CREATE TABLE IF NOT EXISTS uzytkownik_ulubione (ulubione_id INTEGER PRIMARY KEY AUTOINCREMENT, user_id INTEGER, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE NO ACTION, FOREIGN KEY (user_id) REFERENCES uzytkownik(user_id) ON UPDATE CASCADE ON DELETE NO ACTION);" +
                    "CREATE TABLE IF NOT EXISTS oferta_benefity (benefit_id INTEGER PRIMARY KEY AUTOINCREMENT, benefit TEXT, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS oferta_wymagania (wymog_id INTEGER PRIMARY KEY AUTOINCREMENT, wymog TEXT, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS oferta_obowiazki (obowiazek_id INTEGER PRIMARY KEY AUTOINCREMENT, obowiazek TEXT, oferta_id INTEGER, FOREIGN KEY (oferta_id) REFERENCES oferta(oferta_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_certyfikaty (certyfikat_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, certyfikat_nazwa TEXT, organizator TEXT, certyfikat_data DATE, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_wyksztalcenie (wyksztalcenie_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, nazwa_szkoly TEXT, lokalizacja_szkoly TEXT, poziom_wyksztalcenia TEXT, kierunek TEXT, okres_od DATE, okres_do DATE, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_doswiadczenie (doswiadczenie_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, stanowisko TEXT, nazwa_firmy TEXT, lokalizacja TEXT, okres_od DATE, okres_do DATE, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_jezyki (jezyk_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, jezyk TEXT, jezyk_poziom TEXT, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_umiejetnosci (umiejetnosc_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, umiejetnosc TEXT, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);" +
                    "CREATE TABLE IF NOT EXISTS profil_urls (url_id INTEGER PRIMARY KEY AUTOINCREMENT, profil_id INTEGER, url_strona TEXT, url TEXT, FOREIGN KEY (profil_id) REFERENCES profil(profil_id) ON UPDATE CASCADE ON DELETE CASCADE);";

                var createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        public static void AddCategory(Category category)
        {
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO kategoria VALUES(NULL, @Name);";
                insertCommand.Parameters.AddWithValue("@Name", category.Name);
                insertCommand.ExecuteReader();
            }
        }

        public static void AddNewUser(User user)
        {
            if (CanUseEmail(user.Email))
            {
                using (var db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    var insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT INTO uzytkownik VALUES(NULL, @Email, @Password, @IsAdmin)";
                    insertCommand.Parameters.AddWithValue("@Email", user.Email);
                    insertCommand.Parameters.AddWithValue("@Password", user.Password);
                    insertCommand.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                    insertCommand.ExecuteReader();
                }
            }
        }

        public static List<User> LogIn(User user)
        {
            List<User> userList = new List<User>();
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM uzytkownik WHERE email=@Email;";
                insertCommand.Parameters.AddWithValue("@Email", user.Email);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int userID = reader.GetInt32(0);
                            string email = reader.GetString(1);
                            string password = reader.GetString(2);
                            bool isAdmin = reader.GetBoolean(3);

                            if (password == user.Password)
                            {
                                User readUser = new User(userID, email, password, isAdmin);
                                userList.Add(readUser);
                            }
                            else
                                userList = null;
                        }
                        return userList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private static bool CanUseEmail(string email)
        {
            bool checkedEmail = true;
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT COUNT(*) FROM uzytkownik WHERE email=@Email;";
                insertCommand.Parameters.AddWithValue("@Email", email);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int result = reader.GetInt32(0);
                        if (result > 0)
                        {
                            //MessageBox.Show("zajete!");
                            checkedEmail = false;
                        }
                    }
                }
                return checkedEmail;
            }
        }

        public static List<User> GetUserByID(int id)
        {
            List<User> user = new List<User>();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                var insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "SELECT * FROM uzytkownik WHERE user_id=@ID";
                insertCommand.Parameters.AddWithValue("@ID", id);
                using (SqliteDataReader reader = insertCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userID = reader.GetInt32(0);
                        string email = reader.GetString(1);
                        string password = reader.GetString(2);
                        bool isAdmin = reader.GetBoolean(3);

                        User readUser = new User(userID, email, password, isAdmin);
                        user.Add(readUser);
                    }
                    return user;
                }
            }
        }

    }
}
