using System;
using System.Data.SQLite;
using POOActivity.Entities;

namespace POOActivity
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=meuBancoDeDados.sqlite;Version=3";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = "CREATE TABLE IF NOT EXISTS users (CPF TEXT PRIMARY KEY, Name TEXT)";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                string createTableBooksQuery = "CREATE TABLE IF NOT EXISTS bookss (Id INTEGER PRIMARY KEY, Title TEXT, Author TEXT, Available INTEGER NOT NULL DEFAULT 0)";
                using (SQLiteCommand command = new SQLiteCommand(createTableBooksQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                string createTableMagazinesQuery = "CREATE TABLE IF NOT EXISTS magazines (Id INTEGER PRIMARY KEY, Title TEXT, Edicao TEXT, Available INTEGER NOT NULL DEFAULT 0)";
                using (SQLiteCommand command = new SQLiteCommand(createTableMagazinesQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                string createLoansTableQuery = "CREATE TABLE IF NOT EXISTS loans (LoanId INTEGER PRIMARY KEY AUTOINCREMENT, CPF TEXT, BookId INTEGER, DateLoaned DATE, DateReturned DATE, FOREIGN KEY(CPF) REFERENCES users(CPF), FOREIGN KEY(BookId) REFERENCES books(Id))";
                using (SQLiteCommand command = new SQLiteCommand(createLoansTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                void RegisterUser(SQLiteConnection connection)
                {
                    Console.WriteLine("Qual seu nome?");
                    string name = Console.ReadLine();
                    Console.WriteLine("Qual seu CPF?");
                    string cpf = (Console.ReadLine());
                    string insertDataQuery = $"INSERT INTO users (Name, CPF) VALUES ('{name}', {cpf})";
                    using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                void RegisterBook(SQLiteConnection connection)
                {
                    Console.WriteLine("Qual nome do livro?");
                    string name = Console.ReadLine();
                    Console.WriteLine("Qual o autor?");
                    string author = Console.ReadLine();
                    Book book = new Book(name, author);
                    string insertDataQuery = $"INSERT INTO bookss (Title, Author, Available) VALUES ('{name}', '{author}', 1)";
                    using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                }
                void RegisterMagazine(SQLiteConnection connection)
                {
                    Console.WriteLine("Qual nome da Revista?");
                    string name = Console.ReadLine();
                    Console.WriteLine("Qual nome a Edição dela?");
                    string edition = Console.ReadLine();
                    Magazine magazine = new Magazine(name, edition);
                    string insertDataQuery = $"INSERT INTO users (Name, CPF) VALUES ('{name}', '{edition})'";
                    using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("Livro registrado com sucesso!");
                }
                void ShowData()
                {
                    string selectDataQuery = "SELECT * FROM bookss";
                    using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($" Livro Id: {reader["Id"]}, Titulo: {reader["Title"]}, Disponivel 0 sim, 1 não: {reader["Available"]}");
                            }
                        }
                    }
                    string selectUserQuery = "SELECT * FROM magazines";
                    using (SQLiteCommand command = new SQLiteCommand(selectUserQuery, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($" Revista Id: {reader["Id"]}, Title: {reader["Title"]}, Edição: {reader["Edicao"]}, Disponivel 0 sim, 1 não: {reader["Available"]}");
                            }
                        }
                    }
                }
                static void LoanBook(SQLiteConnection connection)
                {
                    Console.WriteLine("Qual o CPF do usuário?");
                    string cpf = Console.ReadLine();
                    Console.WriteLine("Qual o ID do livro?");
                    int bookId = int.Parse(Console.ReadLine());

                    string updateBookQuery = $"UPDATE bookss SET Available = 0 WHERE Id = {bookId} AND Available = 1";
                    using (SQLiteCommand command = new SQLiteCommand(updateBookQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("O livro não está disponível para empréstimo.");
                            return;
                        }
                    }


                    string insertLoanQuery = $"INSERT INTO loans (CPF, BookId, DateLoaned) VALUES ('{cpf}', {bookId}, '{DateTime.Now.ToString("yyyy-MM-dd")}')";
                    using (SQLiteCommand command = new SQLiteCommand(insertLoanQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Empréstimo registrado com sucesso!");
                }


                static void ReturnBook(SQLiteConnection connection)
                {
                    Console.WriteLine("Qual o ID do livro?");
                    int bookId = int.Parse(Console.ReadLine());

                    // Atualiza a disponibilidade do livro
                    string updateBookQuery = $"UPDATE bookss SET Available = 1 WHERE Id = {bookId}";
                    using (SQLiteCommand command = new SQLiteCommand(updateBookQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Atualiza a data de devolução no empréstimo
                    string updateLoanQuery = $"UPDATE loans SET DateReturned = '{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE BookId = {bookId} AND DateReturned IS NULL";
                    using (SQLiteCommand command = new SQLiteCommand(updateLoanQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Devolução registrada com sucesso!");
                }

                int choice;
                do
                {
                    Console.WriteLine("Olá, bem-vindo(a) a biblioteca municipal, oque deseja fazer?");
                    Console.WriteLine("1. Cadastrar Usuario");
                    Console.WriteLine("2. Cadastrar Livro");
                    Console.WriteLine("3. Cadastrar Revista");
                    Console.WriteLine("4. Visualizar itens");
                    Console.WriteLine("5. Fazer emprestimo");
                    Console.WriteLine("6. Devolver emprestimo");
                    Console.WriteLine("6. Sair");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            RegisterUser(connection);
                            break;
                        case 2:
                            RegisterBook(connection);
                            break;
                        case 3:
                            RegisterMagazine(connection);
                            break;
                        case 4:
                            ShowData();
                            break;
                        case 5:
                            LoanBook(connection);
                            break;
                        case 6:
                            ReturnBook(connection);
                            break;
                        case 7:
                            break;
                    }
                } while (choice != 7);




            }
        }
        }
    }
    

