// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Marketplace.Core.Model;
using Microsoft.Data.Sqlite;

namespace Marketplace.Dal
{
    internal class MarketplaceDb : IMarketplaceDb, IDisposable
    {
        private readonly SqliteConnection _connection;

        public MarketplaceDb()
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".."));
            _connection = new SqliteConnection($@"Data Source={path}\Marketplace.Dal\marketplace.db");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task<User[]> GetUsersAsync()
        {
            await using var command = new SqliteCommand(
                "SELECT U.Id, U.Username, COUNT(O.Id) AS Offers\r\n" +
                "FROM User U LEFT JOIN Offer O ON U.Id = O.UserId\r\n" +
                "GROUP BY U.Id, U.Username;",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();


                var results = new List<User>();

                while (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username"))
                    };

                    results.Add(user);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            string sql = "SELECT Id, Username FROM User WHERE Username = @Username;";

            await using var command = new SqliteCommand(sql, _connection);
            command.Parameters.AddWithValue("@Username", username);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username"))
                    };

                    return user;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            string sql = "SELECT Id, Username FROM User WHERE Id = @Id;";

            await using var command = new SqliteCommand(sql, _connection);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username"))
                    };

                    return user;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<User> AddUserAsync(User user)
        {
            string sql = "INSERT INTO User (Username) VALUES (@Username); SELECT last_insert_rowid();";

            await using var command = new SqliteCommand(sql, _connection);
            command.Parameters.AddWithValue("@Username", user.Username);

            try
            {
                await _connection.OpenAsync();
                user.Id = (int)(long)await command.ExecuteScalarAsync();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


        public async Task<Offer> AddOfferAsync(Offer offer, int userId)
        {
            string sql = @"
                INSERT INTO Offer (Id, CategoryId, Description, Location, PictureUrl, PublishedOn, Title, UserId) 
                VALUES (@Id, @CategoryId, @Description, @Location, @PictureUrl, @PublishedOn, @Title, @UserId);
            ";

            offer.Id = Guid.NewGuid();
            offer.UserId = userId;

            await using var command = new SqliteCommand(sql, _connection);
            command.Parameters.AddWithValue("@Id", offer.Id);
            command.Parameters.AddWithValue("@CategoryId", offer.CategoryId);
            command.Parameters.AddWithValue("@Description", offer.Description);
            command.Parameters.AddWithValue("@Location", offer.Location);
            command.Parameters.AddWithValue("@PictureUrl", offer.PictureUrl);
            command.Parameters.AddWithValue("@PublishedOn", offer.PublishedOn);
            command.Parameters.AddWithValue("@Title", offer.Title);
            command.Parameters.AddWithValue("@UserId", offer.UserId);

            try
            {
                await _connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return offer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            string sql = @"
                INSERT INTO Category (Id, Name) 
                VALUES (@Id, @Name);
            ";

            category.Id = (byte)(new Random().Next(1, 255)); // Generate a random byte for the Id

            await using var command = new SqliteCommand(sql, _connection);
            command.Parameters.AddWithValue("@Id", category.Id);
            command.Parameters.AddWithValue("@Name", category.Name);

            try
            {
                await _connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return category;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Offer> GetOfferAsync(Guid id)
        {
            string sql = "SELECT * FROM Offer WHERE Id = @Id;";

            await using var command = new SqliteCommand(sql, _connection);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var offer = new Offer
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        CategoryId = reader.GetByte(reader.GetOrdinal("CategoryId")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Location = reader.GetString(reader.GetOrdinal("Location")),
                        PictureUrl = reader.GetString(reader.GetOrdinal("PictureUrl")),
                        PublishedOn = reader.GetDateTime(reader.GetOrdinal("PublishedOn")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                    };

                    return offer;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }   

        public async Task<Category> GetCategoryAsync(byte id)
        {
            string sql = "SELECT * FROM Category WHERE Id = @Id;";

            await using var command = new SqliteCommand(sql, _connection);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                await _connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var category = new Category
                    {
                        Id = reader.GetByte(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };

                    return category;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}