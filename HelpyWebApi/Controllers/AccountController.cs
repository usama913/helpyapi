using HelpyWebApi.Controllers.Helper;
using HelpyWebApi.Data;
using HelpyWebApi.Models;
using HelpyWebApi.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace HelpyWebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly HelpyDbContext _context;
        private readonly SignInManager<Users> signInManager;

        public AccountController(HelpyDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("api/account/registerthirdpartyuser")]
        public async Task<IActionResult> RegisterThirdPartyUser([FromBody] UserRegistration model)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for existing user
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                return Conflict(new { Message = "User already exists" });
            }

            //Validate images
            if (model.Images == null || model.Images.Count < 1 || model.Images.Count > 6)
            {
                return BadRequest(new { Message = "Please upload between 1 and 6 images." });
            }

            try
            {
                // Create User entity
                var user = new Users
                {
                    UGuid = model.UGuid,
                    FullName = model.FullName,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    Gender = model.Gender,
                    Occupation = model.Occupation,
                    Age = model.Age,
                    Birthday = Convert.ToDateTime(model.Birthday),
                    Ethnicity = model.Ethnicity,
                    Sexuality = model.Sexuality,
                    Description = model.Description,
                    Type = model.Type,
                    IsActive = true,
                    SubscriptionId = 1,
                    ExpireDate = DateTime.Now.AddMonths(6),
                    PackageRenewalDate = DateTime.Now.AddMonths(6),
                    CreatedDate = DateTime.Now,
                    UserStatus = true,
                    City = model.City,
                    Country = model.Country,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // Create UserDetails entity
                var userDetail = new UserDetails
                {
                    UserId = user.Id,
                    UGuid = model.UGuid,
                    AgeRangeMin = model.UseradditionalData.AgeRange[0],
                    AgeRangeMax = model.UseradditionalData.AgeRange[1],
                    BodyType = model.UseradditionalData.BodyType,
                    Children = model.UseradditionalData.Children,
                    Drinking = model.UseradditionalData.Drinking,
                    Education = model.UseradditionalData.Education,
                    HeightInInches = model.UseradditionalData.HeightInInches,
                    Language = model.UseradditionalData.Language,
                    RelationshipStatus = model.UseradditionalData.RelationshipStatus,
                    Smoking = model.UseradditionalData.Smoking
                };
                await _context.UserDetails.AddAsync(userDetail);

                // Save selected bills
                var userBillsDetails = model.SelectedItems.Select(item => new UserBillDetails
                {
                    UserId = user.Id,
                    BillId = item
                }).ToList();

                await _context.UserBillsDetails.AddRangeAsync(userBillsDetails);

                // Define the directory path
                // Create Image and UserImageDetail entries
                var imageEntities = new List<Images>();

                foreach (var image in model.Images)
                {
                    var imageEntity = new Images
                    {
                        Name = image.Key,
                        ImageLink = image.Value,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        StatusID = 1,
                        UserId = user.Id
                    };

                    imageEntities.Add(imageEntity);
                }

                // Save all images at once
                await _context.Images.AddRangeAsync(imageEntities);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500,
                    new { Message = "An error occurred during registration. Please try again later." });
            }
        }
        [HttpGet]
        [Route("api/account/GetUserDetail")]
        public async Task<IActionResult> GetUserDetail(string uGuid)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UGuid == uGuid);
            if (existingUser == null)
            {
                return BadRequest(new { Message = "User does't exists" });
            }
            var result = from u in _context.Users
                         join s in _context.Subscriptions on u.SubscriptionId equals s.Id
                         join urd in _context.UserDetails on u.Id equals urd.UserId
                         join img in _context.Images on urd.UserId equals img.UserId
                         where u.UGuid == uGuid
                         select new GetUserDetailsCiewModal
                         {
                             Users = u,
                             UserDetails = urd,
                             Image = img,
                             Subscription = s
                         };

            var registrationList = await result.ToListAsync();

            return Ok(registrationList);

        }
        [HttpGet]
        [Route("api/account/GetAllBills")]
        public async Task<IActionResult> GetAllBills()
        {
            var allbills = await _context.Bills.ToListAsync();

            return Ok(allbills);

        }
        [HttpGet]
        [Route("api/account/GetUserBills")]
        public async Task<IActionResult> GetAllBills(int userId)
        {
            var result = from b in _context.Bills
                         join ubd in _context.UserBillsDetails on b.Id equals ubd.BillId
                         where ubd.UserId == userId
                         select new BillsViewModal
                         {
                             Bills = b,
                             UserBillDetails = ubd
                         };

            return Ok(result);

        }

        [HttpPost]
        [Route("api/account/EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserEditModel model)
        {
         
            var imageEntities = new List<Images>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.userId);
                var userdetail = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == model.userId);
                if (user == null)
                {
                    return BadRequest(new { Message = "User does't exists" });
                }
                else
                {
                    user.FullName = model.FullName!=null ? model.FullName : user.FullName;
                    user.Description = model.Description != null ? model.Description : user.Description;
                    user.Sexuality = model.Sexuality != null ? model.Sexuality : user.Sexuality;
                    user.Occupation = model.Occupation != null ? model.Occupation : user.Occupation;
                    user.Gender = model.Gender != null ? model.Gender : user.Gender;
                    user.Ethnicity = model.Ethnicity != null ? model.Ethnicity : user.Ethnicity;
                    user.Birthday = model.Birthday != null ? model.Birthday : user.Birthday;

                    userdetail.Language = model.Language != null ? model.Language : userdetail.Language;
                    userdetail.BodyType = model.BodyType != null ? model.BodyType : userdetail.BodyType;
                    userdetail.RelationshipStatus = model.RelationshipStatus != null ? model.RelationshipStatus : userdetail.RelationshipStatus;
                    userdetail.Smoking = model.Smoking != null ? model.Smoking : userdetail.Smoking;
                    userdetail.Children = model.Children != null ? model.Children : userdetail.Children;
                    userdetail.Drinking = model.Drinking != null ? model.Drinking : userdetail.Drinking;
                    userdetail.Education = model.Education != null ? model.Education : userdetail.Education;
                    userdetail.HeightInInches = model.HeightInInches != null ? model.HeightInInches : userdetail.HeightInInches;

                    await _context.SaveChangesAsync();
                    // Link images to the user
                    if (model.Images != null && model.Images.Count > 0)
                    {
                        foreach (var image in model.Images)
                        {
                            if (image.HasValue)  // Check if the nullable KeyValuePair has a value
                            {
                                var imageValue = image.Value;  // Get the actual KeyValuePair

                                var existingImage = await _context.Images.FirstOrDefaultAsync(img =>
                                    img.Name == imageValue.Key && img.UserId == model.userId);

                                if (existingImage != null)
                                {
                                    existingImage.Name = imageValue.Key;
                                    existingImage.ImageLink = imageValue.Value;
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    var imageEntity = new Images
                                    {
                                        Name = imageValue.Key,
                                        ImageLink = imageValue.Value,
                                        CreatedDate = DateTime.Now,
                                        UpdateDate = DateTime.Now,
                                        StatusID = 1,
                                        UserId = model.userId
                                    };
                                    imageEntities.Add(imageEntity);
                                }
                            }
                        }

                        await _context.Images.AddRangeAsync(imageEntities);
                        await _context.SaveChangesAsync();
                    }

                    if (model.SelectedItems != null && model.SelectedItems.Count > 0)
                    {
                        if (model.SelectedItems.Any(item => item != 0))
                        {
                            var existingBills = await _context.UserBillsDetails
                                .Where(b => b.UserId == user.Id && model.SelectedItems.Contains(b.BillId))
                                .Select(b => b.BillId)
                                .ToListAsync();

                            // Filter out bills that are already assigned
                            var newBills = model.SelectedItems
                                .Where(item => !existingBills.Contains(item))
                                .Select(item => new UserBillDetails
                                {
                                    UserId = user.Id,
                                    BillId = item
                                })
                                .ToList();

                            // Add only new bills
                            if (newBills.Count > 0)
                            {
                                await _context.UserBillsDetails.AddRangeAsync(newBills);
                                await _context.SaveChangesAsync();
                            }

                            await _context.UserBillsDetails.AddRangeAsync(newBills);
                            await _context.SaveChangesAsync();
                        }

                        // Fetch existing bills for the user

                    }

                    return Ok(user);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { Message = $"Failed to process request contact your administrator ex = {exception.Message}" });
            }
        }

        [HttpPost]
        [Route("api/account/AddUserBills")]
        public async Task<IActionResult> AddBills([FromBody] AddUserBillsRequest request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
                if (user == null)
                {
                    return BadRequest(new { Message = "User doesn't exist" });
                }
                else
                {
                    if (request.SelectedItems != null && request.SelectedItems.Count > 0)
                    {
                        // Fetch existing bills for the user
                        var existingBills = await _context.UserBillsDetails
                            .Where(b => b.UserId == request.UserId && request.SelectedItems.Contains(b.BillId))
                            .Select(b => b.BillId)
                            .ToListAsync();

                        // Filter out bills that are already assigned
                        var newBills = request.SelectedItems
                            .Where(item => !existingBills.Contains(item))
                            .Select(item => new UserBillDetails
                            {
                                UserId = user.Id,
                                BillId = item
                            })
                            .ToList();

                        // Add only new bills
                        if (newBills.Count > 0)
                        {
                            await _context.UserBillsDetails.AddRangeAsync(newBills);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return Ok(new { Message = "All selected bills already exist for the user." });
                        }
                    }

                    return Ok(new { Message = "New bills added successfully." });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { Message = $"Failed to process request. Contact your administrator. Error: {exception.Message}" });
            }
        }
        [HttpPost]
        [Route("api/account/DeleteUserBills")]
        public async Task<IActionResult> AddBills(int userId, int billId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return BadRequest(new { Message = "User does't exists" });
                }
                else
                {
                    if (billId > 0)
                    {
                        // Fetch the UserBillsDetails entry based on the userId and billId
                        var userBillDetails = await _context.UserBillsDetails
                            .FirstOrDefaultAsync(b => b.UserId == userId && b.BillId == billId);

                        if (userBillDetails == null)
                        {
                            return NotFound(new { Message = "Bill not found for the user" });
                        }

                        // Remove the selected bill
                        _context.UserBillsDetails.Remove(userBillDetails);
                        await _context.SaveChangesAsync();
                    }

                    return Ok(new { Message = "Bill deleted successfully" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { Message = $"Failed to process request contact your administrator ex = {exception.Message}" });
            }
        }

        [HttpPost]
        [Route("GetAllUserDetails")]
        public async Task<IActionResult> GetAllUserDetails([FromBody] FilterUserModel model)
        {
            string csvSelectedItems = String.Empty;
            if (model.SelectedItems != null && model.SelectedItems.Count > 0)
            {
                if (model.SelectedItems.Any(item => item != 0))
                {
                    csvSelectedItems = String.Join(", ", model.SelectedItems);
                }
            }

            var userDetails = await _context.GetAllUserDetailsAsync(model, csvSelectedItems);
            return Ok(userDetails);
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync(string gender, string sexuality)
        {
            var userDetails = await _context.GetAllUsersAsync(gender, sexuality);
            return Ok(userDetails);
        }

        [HttpPost]
        [Route("api/AddFavouriteUser")]
        public async Task<IActionResult> AddFavourite(int userId,int FavouriteUserId)
        {
            var favouriteUser = await _context.Favourites.FirstOrDefaultAsync(u => u.Id == userId && u.FavouriteUserId == FavouriteUserId);
            if (favouriteUser != null)
            {
                return BadRequest(new { Message = "User already in your favourite list." });
            }
            var newFavourite = new Favourites
            {
                UserId = userId,
                FavouriteUserId = FavouriteUserId
            };

            _context.Favourites.Add(newFavourite);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User added to favourites successfully." });
        }
        [HttpPost]
        [Route("api/AddFollowUser")]
        public async Task<IActionResult> AddFollowUser(int userId, int FollowUserId)
        {
            var favouriteUser = await _context.Followes.FirstOrDefaultAsync(u => u.Id == userId && u.FollowUserId == FollowUserId);
            if (favouriteUser != null)
            {
                return BadRequest(new { Message = "User already in your followed list." });
            }
            var newFollow = new Follow()
            {
                UserId = userId,
                FollowUserId = FollowUserId
            };

            _context.Followes.Add(newFollow);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User added to followed successfully." });
        }
        [HttpPost]
        [Route("api/GetFavoriteUsers")]
        public async Task<List<GetFavouriteUsers>> GetFavoriteUsersAsync(int userId)
        {
            var userDetails = await _context.GetFavouriteUsers(userId);
            return userDetails;
        }
        [HttpPost]
        [Route("api/GetFollowUsers")]
        public async Task<List<GetFollowUsers>> GetFollowedUsersAsync(int userId)
        {
            var userDetails = await _context.GetFollowedUsers(userId);
            return userDetails;
        }
        [HttpGet]
        [Route("api/GetUserById")]
        public async Task<List<GetAllUserDetails>> GetUserById(int userId)
        {
            var userDetails = await _context.GetUserById(userId);
            return userDetails;
        }
        [HttpPost]
        [Route("api/AddViewUser")]
        public async Task<IActionResult> AddViewUser(int userId, int ViewUserId)
        {
            var viewedUser = await _context.Viewed.FirstOrDefaultAsync(u => u.Id == userId && u.ViewedUserId == ViewUserId);
            if (viewedUser != null)
            {
                return BadRequest(new { Message = "User already viewed list." });
            }
            var newView = new Viewed()
            {
                UserId = userId,
                ViewedUserId = ViewUserId
            };

            _context.Viewed.Add(newView);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User added to Viewed successfully." });
        }

        [HttpPost]
        [Route("api/GetUserViewedList")]
        public async Task<List<GetViewedUsers>> GetUserViewedListAsync(int userId)
        {
            //Get Users whos profile viewed by user
            var userDetails = await _context.GetUserViewedList(userId);
            return userDetails;
        }
        [HttpGet]
        [Route("api/GetUserViewsList")]
        //Get Users who viewed user profile
        public async Task<List<GetViewedUsers>> GetUserViewsListAsync(int userId)
        {
            var userDetails = await _context.GetUserViewsList(userId);
            return userDetails;
        }
        [HttpGet]
        [Route("api/GetUserFsmToken")]
        public async Task<GetUserToken> GetUserFsmToken(int userId)
        {
            var userDetails = await _context.UserTokens.FirstOrDefaultAsync(u => u.UserId == userId);
            var usertoken = new GetUserToken()
            {
                LoginProvider = userDetails.LoginProvider,
                Name = userDetails.Name,
                UserId = userDetails.UserId,
                Value = userDetails.Value
            };
            return usertoken;
        }
        [HttpPost]
        [Route("api/CreateUserFsmToken")]
        public async Task<IActionResult> CreateUserFsmToken([FromBody] GetUserToken model)
        {
            var userDetails = await _context.UserTokens.FirstOrDefaultAsync(u => u.UserId == model.UserId);
            if (userDetails != null)
            {
                userDetails.Value = model.Value;
                await _context.SaveChangesAsync();
                return Ok(userDetails);
            }
            else
            {
                var usertoken = new IdentityUserToken<int>
                {
                    LoginProvider = model.LoginProvider,
                    Name = model.Name,
                    UserId = model.UserId,
                    Value = model.Value
                };
                _context.UserTokens.Add(usertoken);
                await _context.SaveChangesAsync();
                return Ok(usertoken);
            }
            
        }

        [HttpGet]
        [Route("api/GetUserLikes")]
        //Get Users who viewed user profile
        public async Task<List<GetViewedUsers>> GetUserLikesListAsync(int userId)
        {
            var userDetails = await _context.GetUserLikes(userId);
            return userDetails;
        }
    }

}