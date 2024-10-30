using HelpyWebApi.Models;
using HelpyWebApi.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using static System.Net.Mime.MediaTypeNames;

namespace HelpyWebApi.Data
{
    public class HelpyDbContext : IdentityDbContext<Users, Roles, int>
    {
        public HelpyDbContext(DbContextOptions<HelpyDbContext> options) : base(options)
        {
        }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<UserBillDetails> UserBillsDetails { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Follow> Followes { get; set; }
        public DbSet<Viewed> Viewed { get; set; }

        public async Task<List<GetAllUserDetails>> GetAllUserDetailsAsync(FilterUserModel model, string csvSelectedItems)
        {
            var userIdParam = new MySqlParameter("@inputUserId", model.userId ?? (object)DBNull.Value);
            var minAgeParam = new MySqlParameter("@inputMinAge", model.MinAge ?? (object)DBNull.Value);
            var maxAgeParam = new MySqlParameter("@inputMaxAge", model.MaxAge ?? (object)DBNull.Value);
            var genderParam = new MySqlParameter("@inputGender", model.Gender ?? (object)DBNull.Value);
            var occupationParam = new MySqlParameter("@inputOccupation", model.Occupation ?? (object)DBNull.Value);
            var bodyTypeParam = new MySqlParameter("@inputBodyType", model.BodyType ?? (object)DBNull.Value);
            var childrenParam = new MySqlParameter("@inputChildren", model.Children ?? (object)DBNull.Value);
            var drinkingParam = new MySqlParameter("@inputDrinking", model.Drinking ?? (object)DBNull.Value);
            var educationeParam = new MySqlParameter("@inputEducation", model.Education ?? (object)DBNull.Value);
            var ethnicityParam = new MySqlParameter("@inputEthnicity", model.Ethnicity ?? (object)DBNull.Value);
            var fullNameParam = new MySqlParameter("@inputFullName", model.FullName ?? (object)DBNull.Value);
            var heightInInchesParam = new MySqlParameter("@inputHeightInInches", model.HeightInInches ?? (object)DBNull.Value);
            var languageParam = new MySqlParameter("@inputLanguage", model.Language ?? (object)DBNull.Value);
            var relationshipStatusParam = new MySqlParameter("@inputRelationshipStatus", model.RelationshipStatus ?? (object)DBNull.Value);
            var sexualityParam = new MySqlParameter("@inputSexuality", model.Sexuality ?? (object)DBNull.Value);
            var smokingParam = new MySqlParameter("@inputSmoking", model.Smoking ?? (object)DBNull.Value);
            var selectedItemsParam = new MySqlParameter("@inputSelectedItems", csvSelectedItems ?? (object)DBNull.Value);
            var cityParam = new MySqlParameter("@inputCity", model.City ?? (object)DBNull.Value);
            var countryParam = new MySqlParameter("@inputCountry", model.Country ?? (object)DBNull.Value);
            var longitudeParam = new MySqlParameter("@inputLongitude", model.Longitude ?? (object)DBNull.Value);
            var latitudeParam = new MySqlParameter("@inputLatitude", model.Latitude ?? (object)DBNull.Value);

            var query = "CALL GetUserDetails(@inputUserId,@inputMinAge,@inputMaxAge, @inputGender, @inputOccupation,@inputBodyType,@inputChildren,@inputEducation,@inputEthnicity,@inputFullName,@inputHeightInInches,@inputLanguage,@inputRelationshipStatus,@inputSexuality,@inputSmoking,@inputSelectedItems,@inputCity,@inputCountry,@inputLongitude,@inputLatitude)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(userIdParam);
                command.Parameters.Add(minAgeParam);
                command.Parameters.Add(maxAgeParam);
                command.Parameters.Add(genderParam);
                command.Parameters.Add(occupationParam);
                command.Parameters.Add(bodyTypeParam);
                command.Parameters.Add(childrenParam);
                command.Parameters.Add(drinkingParam);
                command.Parameters.Add(educationeParam);
                command.Parameters.Add(ethnicityParam);
                command.Parameters.Add(fullNameParam);
                command.Parameters.Add(heightInInchesParam);
                command.Parameters.Add(languageParam);
                command.Parameters.Add(relationshipStatusParam);
                command.Parameters.Add(sexualityParam);
                command.Parameters.Add(smokingParam);
                command.Parameters.Add(selectedItemsParam);
                command.Parameters.Add(cityParam);
                command.Parameters.Add(countryParam);
                command.Parameters.Add(longitudeParam);
                command.Parameters.Add(latitudeParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetAllUserDetails>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetAllUserDetails
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }

        public async Task<List<GetAllUserDetails>> GetAllUsersAsync(string gender, string sexuality)
        {
            var genderParam = new MySqlParameter("@inputGender", gender ?? (object)DBNull.Value);
            var sexualityParam = new MySqlParameter("@inputSexuality", sexuality ?? (object)DBNull.Value);


            var query = "CALL GetAllUser(@inputGender,@inputSexuality)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(genderParam);
                command.Parameters.Add(sexualityParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetAllUserDetails>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetAllUserDetails
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }

        public async Task<List<GetFavouriteUsers>> GetFavouriteUsers(int userId)
        {
            var userIdParam = new MySqlParameter("@inputUserId", userId);

            var query = "CALL GetFavouriteUserDetails(@inputUserId)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(userIdParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetFavouriteUsers>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetFavouriteUsers
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }

        public async Task<List<GetAllUserDetails>> GetUserById(int userId)
        {
            var userIdParam = new MySqlParameter("@inputUserId", userId);

            var query = "CALL GetUserById(@inputUserId)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(userIdParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetAllUserDetails>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetAllUserDetails
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }

        public async Task<List<GetFollowUsers>> GetFollowedUsers(int userId)
        {
            var userIdParam = new MySqlParameter("@inputUserId", userId);

            var query = "CALL GetFollowUserDetails(@inputUserId)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(userIdParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetFollowUsers>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetFollowUsers
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }

        public async Task<List<GetViewedUsers>> GetUserViewedList(int userId)
        {
            var userIdParam = new MySqlParameter("@inputUserId", userId);

            var query = "CALL GetViewedUsers(@inputUserId)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(userIdParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetViewedUsers>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetViewedUsers
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }

        public async Task<List<GetViewedUsers>> GetUserViewsList(int userId)
        {
            var userIdParam = new MySqlParameter("@inputUserId", userId);

            var query = "CALL GetUserViews(@inputUserId)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(userIdParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetViewedUsers>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetViewedUsers
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }

        public async Task<List<GetViewedUsers>> GetUserLikes(int userId)
        {
            var userIdParam = new MySqlParameter("@inputUserId", userId);

            var query = "CALL GetUserLikes(@inputUserId)";

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(userIdParam);

                this.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var userDetails = new List<GetViewedUsers>();

                    while (await result.ReadAsync())
                    {
                        // Manually map fields from the result to the DTO
                        userDetails.Add(new GetViewedUsers
                        {
                            UserId = result.GetInt32(result.GetOrdinal("UserId")),
                            FullName = result.IsDBNull(result.GetOrdinal("FullName")) ? null : result.GetString(result.GetOrdinal("FullName")),
                            UGuid = result.IsDBNull(result.GetOrdinal("UGuid")) ? null : result.GetString(result.GetOrdinal("UGuid")),
                            IsActive = result.GetBoolean(result.GetOrdinal("IsActive")),
                            UserStatus = result.GetBoolean(result.GetOrdinal("UserStatus")),
                            SubscriptionId = result.GetInt32(result.GetOrdinal("SubscriptionId")),
                            Gender = result.IsDBNull(result.GetOrdinal("Gender")) ? null : result.GetString(result.GetOrdinal("Gender")),
                            Occupation = result.IsDBNull(result.GetOrdinal("Occupation")) ? null : result.GetString(result.GetOrdinal("Occupation")),
                            Age = result.GetInt32(result.GetOrdinal("Age")),
                            CreatedDate = result.GetDateTime(result.GetOrdinal("CreatedDate")),
                            Birthday = result.GetDateTime(result.GetOrdinal("Birthday")),
                            Email = result.IsDBNull(result.GetOrdinal("Email")) ? null : result.GetString(result.GetOrdinal("Email")),
                            Ethnicity = result.IsDBNull(result.GetOrdinal("Ethnicity")) ? null : result.GetString(result.GetOrdinal("Ethnicity")),
                            Sexuality = result.IsDBNull(result.GetOrdinal("Sexuality")) ? null : result.GetString(result.GetOrdinal("Sexuality")),
                            Description = result.IsDBNull(result.GetOrdinal("Description")) ? null : result.GetString(result.GetOrdinal("Description")),
                            PhoneNumber = result.IsDBNull(result.GetOrdinal("PhoneNumber")) ? null : result.GetString(result.GetOrdinal("PhoneNumber")),
                            SubscriptionName = result.IsDBNull(result.GetOrdinal("SubscriptionName")) ? null : result.GetString(result.GetOrdinal("SubscriptionName")),
                            SubscriptionPrice = result.GetDecimal(result.GetOrdinal("SubscriptionPrice")),
                            SubsriptionDurationInDays = result.GetInt32(result.GetOrdinal("SubsriptionDurationInDays")),
                            AgeRangeMin = result.GetInt32(result.GetOrdinal("AgeRangeMin")),
                            AgeRangeMax = result.GetInt32(result.GetOrdinal("AgeRangeMax")),
                            BodyType = result.IsDBNull(result.GetOrdinal("BodyType")) ? null : result.GetString(result.GetOrdinal("BodyType")),
                            Children = result.IsDBNull(result.GetOrdinal("Children")) ? null : result.GetString(result.GetOrdinal("Children")),
                            Drinking = result.IsDBNull(result.GetOrdinal("Drinking")) ? null : result.GetString(result.GetOrdinal("Drinking")),
                            Education = result.IsDBNull(result.GetOrdinal("Education")) ? null : result.GetString(result.GetOrdinal("Education")),
                            HeightInInches = result.GetInt32(result.GetOrdinal("HeightInInches")),
                            Language = result.IsDBNull(result.GetOrdinal("Language")) ? null : result.GetString(result.GetOrdinal("Language")),
                            RelationshipStatus = result.IsDBNull(result.GetOrdinal("RelationshipStatus")) ? null : result.GetString(result.GetOrdinal("RelationshipStatus")),
                            Smoking = result.IsDBNull(result.GetOrdinal("Smoking")) ? null : result.GetString(result.GetOrdinal("Smoking")),
                            ImagePaths = result.IsDBNull(result.GetOrdinal("ImagePaths")) ? null : result.GetString(result.GetOrdinal("ImagePaths")).Split(',').ToList(),
                            Bills = result.IsDBNull(result.GetOrdinal("Bills")) ? null : result.GetString(result.GetOrdinal("Bills")).Split(',').ToList(),
                            City = result.IsDBNull(result.GetOrdinal("City")) ? null : result.GetString(result.GetOrdinal("City")),
                            Country = result.IsDBNull(result.GetOrdinal("Country")) ? null : result.GetString(result.GetOrdinal("Country")),
                            Longitude = result.IsDBNull(result.GetOrdinal("Longitude")) ? null : result.GetString(result.GetOrdinal("Longitude")),
                            Latitude = result.IsDBNull(result.GetOrdinal("Latitude")) ? null : result.GetString(result.GetOrdinal("Latitude")),
                        });
                    }

                    return userDetails;
                }
            }
        }
    }
}
