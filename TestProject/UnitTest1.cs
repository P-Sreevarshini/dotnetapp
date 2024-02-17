using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;




[TestFixture]
public class dotnetappApplicationTests
{
    private HttpClient _httpClient;
    private string _generatedToken;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://8080-dfbbeddfccdbcfacbdcbaeaeaebbbabcf.premiumproject.examly.io/"); 
    }

    [Test] //..............check for customer registration
    public async Task Backend_TestRegisterCustomer()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniquePassword = $"abc@123A";

        string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Customer\"}}";

        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test] //.....................check for customer login

    public async Task Backend_TestLoginUser()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniqueRole = "Customer";

        // Register the user first
        string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"MobileNumber\": \"1234567890\",\"UserRole\": \"{uniqueRole}\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Then try to login
        string requestBody1 = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(requestBody1, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    [Test] //..................check for Admin registration
    public async Task Backend_TestRegisterAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com"; 

        string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test] //................check for Admin login
    public async Task Backend_TestLoginAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniqueRole = "Admin";

        // Register the user first
         string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Then try to login
        string requestBody1 = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(requestBody1, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    // [Test] //..........Check for POST review authorized to Customer
    // public async Task Backend_TestPostReviews()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniquePassword = $"abcdA{uniqueId}@123";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //      string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Customer\"}}";
    //     HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
    //      Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //     var customerLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(customerLoginRequestBody, Encoding.UTF8, "application/json"));
    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    //     string responseString = await loginResponse.Content.ReadAsStringAsync();
    //     dynamic responseMap = JsonConvert.DeserializeObject(responseString);
    //     string customerAuthToken = responseMap.token;

    //     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    //     var review = new
    //     {
    //         Subject = "Test subject",
    //         Body = "Test body",
    //         Rating = 5
    //     };

    //     string requestBody = JsonConvert.SerializeObject(review);
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/review", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    //     Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    // }

    // [Test] //..........Check for GET review authorized to Admin
    // public async Task Backend_TestGetAllReviews()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniquePassword = $"abcdA{uniqueId}@123";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //      string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
    //     HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
    //      Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //     var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    //     string responseString = await loginResponse.Content.ReadAsStringAsync();
    //     dynamic responseMap = JsonConvert.DeserializeObject(responseString);
    //     string adminAuthToken = responseMap.token;

    //     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

    //     HttpResponseMessage getReviewsResponse = await _httpClient.GetAsync("/api/review");
        
    //     Assert.AreEqual(HttpStatusCode.OK, getReviewsResponse.StatusCode);
    // }

    [Test] //.......check for GET resorts 
    public async Task Get_Resorts()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/resort");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Test] //......check for POST resort authorized to Admin
    public async Task Post_Resort()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

         string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        
        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        var resort = new
        {
            ResortName = "Test Resort",
            ResortImageUrl = "test-image-url",
            ResortLocation = "Test Location",
            ResortAvailableStatus = "Available",
            Price = 100,
            Capacity = 50,
            Description = "Test Description"
        };

        string requestBody = JsonConvert.SerializeObject(resort);
        HttpResponseMessage response = await _httpClient.PostAsync("/api/resort", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
        [Test] //.....................check for PUT resort
        public async Task PUT_Resort()
        {
            // Register Admin user
            string uniqueId = Guid.NewGuid().ToString();
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniquePassword = $"abcdA{uniqueId}@123";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string registerRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
            HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

            // Log in as Admin
            string adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

            string responseString = await loginResponse.Content.ReadAsStringAsync();
            dynamic responseMap = JsonConvert.DeserializeObject(responseString);
            string adminAuthToken = responseMap.token;

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

            // Add a new resort
            var resort = new
            {
                ResortName = "Test Resort",
                ResortImageUrl = "test-image-url",
                ResortLocation = "Test Location",
                ResortAvailableStatus = "Available",
                Price = 100,
                Capacity = 50,
                Description = "Test Description"
            };

            string requestBody = JsonConvert.SerializeObject(resort);
            HttpResponseMessage response = await _httpClient.PostAsync("/api/resort", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // Get the list of resorts
            HttpResponseMessage getResortResponse = await _httpClient.GetAsync("api/resort");
            Assert.AreEqual(HttpStatusCode.OK, getResortResponse.StatusCode);
            string getResortResponseBody = await getResortResponse.Content.ReadAsStringAsync();
            var resorts = JsonConvert.DeserializeObject<List<Resort>>(getResortResponseBody);
            Assert.IsNotNull(resorts);
            Assert.IsTrue(resorts.Any());

            // Update the newly added resort
            var updatedResort = new
            {
                ResortName = "Updated Resort Name",
                ResortImageUrl = "updated-image-url",
                ResortLocation = "Updated Location",
                ResortAvailableStatus = "Updated Available Status",
                Price = 200,
                Capacity = 30,
                Description = "Updated Description"
            };

            string updateResortRequestBody = JsonConvert.SerializeObject(updatedResort);

            // Assuming ResortId property exists in Resort model
            long resortIdToUpdate = resorts.FirstOrDefault()?.ResortId ?? 1;
            HttpResponseMessage updateResortResponse = await _httpClient.PutAsync($"api/resort/{resortIdToUpdate}", new StringContent(updateResortRequestBody, Encoding.UTF8, "application/json"));
          //  Assert.AreEqual(HttpStatusCode.OK, updateResortResponse.StatusCode);
        }    

//         [Test]
// public async Task Backend_TestPutReviews()
// {
//     // Generate unique identifiers
//     string uniqueId = Guid.NewGuid().ToString();
//     string uniqueusername = $"abcd_{uniqueId}";
//     string uniquepassword = $"abcdA{uniqueId}@123";
//     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

//     // Register a customer
//     string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"customer\" }}";
//     HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

//     // Login the registered customer
//     string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
//     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
//     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
//     dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
//     string customerAuthToken = loginResponseMap.token;

//     // Use the obtained token in the request to add a review
//     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    
//     // Add a review to update
//     var initialReviewDetails = new
//     {
//         UserId = 1,
//         Subject = "Initial Subject",
//         Body = "Initial Body",
//         Rating = 3,
//         DateCreated = DateTime.Now,
//         User = new
//         {
//             UserId = 0,
//             Email = "string",
//             Password = "string",
//             Username = "string",
//             MobileNumber = "string",
//             Role = "string"
//         }
//     };

//     string initialReviewRequestBody = JsonConvert.SerializeObject(initialReviewDetails);
//     HttpResponseMessage addReviewResponse = await _httpClient.PostAsync("/api", new StringContent(initialReviewRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, addReviewResponse.StatusCode);

//     // Get the added review details
//     string addReviewResponseBody = await addReviewResponse.Content.ReadAsStringAsync();
//     dynamic addReviewResponseMap = JsonConvert.DeserializeObject(addReviewResponseBody);

//     // Handle the potential null value for the review ID
//     int? reviewId = addReviewResponseMap?.reviewId;

//     if (reviewId.HasValue)
//     {
//         // Update the review with the correct reviewId
//         var updatedReviewDetails = new
//         {
//             ReviewId = reviewId,
//             UserId = 1,
//             Subject = "Updated Subject",
//             Body = "Updated Body",
//             Rating = 4,
//             DateCreated = DateTime.Now,
//             User = new
//             {
//                 UserId = 0,
//                 Email = "string",
//                 Password = "string",
//                 Username = "string",
//                 MobileNumber = "string",
//                 Role = "string"
//             }
//         };

//         string updateReviewRequestBody = JsonConvert.SerializeObject(updatedReviewDetails);
//         HttpResponseMessage updateReviewResponse = await _httpClient.PutAsync($"/api/{reviewId}", new StringContent(updateReviewRequestBody, Encoding.UTF8, "application/json"));

//         // Assert that the review is updated successfully
//         if (updateReviewResponse.StatusCode != HttpStatusCode.OK)
//         {
//             // Additional information about the response
//             string responseContent = await updateReviewResponse.Content.ReadAsStringAsync();
//             Console.WriteLine($"Response Content: {responseContent}");
//         }

//         Assert.AreEqual(HttpStatusCode.OK, updateReviewResponse.StatusCode);
//     }
//     else
//     {
//         // Log additional information for debugging
//         string responseContent = await addReviewResponse.Content.ReadAsStringAsync();
//         Console.WriteLine($"Add Review Response Content: {responseContent}");

//         Assert.Fail("Review ID is null or not found in the response.");
//     }
// }    
// [Test] // Check for DELETE resort authorized to admin
// public async Task DeleteResort()
// {
//     // Arrange
//     var uniqueId = Guid.NewGuid().ToString();
//     var uniqueUsername = $"abcd_{uniqueId}";
//     var uniquePassword = $"abcdA{uniqueId}@123";
//     var uniqueEmail = $"abcd{uniqueId}@gmail.com";

//     // Register an admin
//     var registerRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
//     var registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

//     // Log in as admin
//     var loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquePassword}\"}}";
//     var loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

//     var loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
//     var responseMap = JsonConvert.DeserializeObject<Dictionary<string, object>>(loginResponseBody);
//     var authToken = responseMap["token"].ToString();

//     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

//     // Add a resort
//     var resort = new
//     {
//         ResortName = "Test Resort",
//         ResortImageUrl = "test-image-url",
//         ResortLocation = "Test Location",
//         ResortAvailableStatus = "Available",
//         Price = 100,
//         Capacity = 50,
//         Description = "Test Description"
//     };

//     var requestBody = JsonConvert.SerializeObject(resort);
//     var addResortResponse = await _httpClient.PostAsync("/api/Resort", new StringContent(requestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.Created, addResortResponse.StatusCode);

//     // Get the ID of the added resort
//     var addedResortResponseBody = await addResortResponse.Content.ReadAsStringAsync();
//     var addedResort = JsonConvert.DeserializeObject<Resort>(addedResortResponseBody);
//     var resortIdToDelete = addedResort.ResortId;

//     // Act: Delete the resort
//     var deleteResortResponse = await _httpClient.DeleteAsync($"/api/Resort/{resortIdToDelete}");

//     // Assert
//     Assert.AreEqual(HttpStatusCode.OK, deleteResortResponse.StatusCode);

//     // Verify that the resort is deleted
//     var verifyDeleteResponse = await _httpClient.GetAsync($"/api/Resort/{resortIdToDelete}");
//     Assert.AreEqual(HttpStatusCode.NotFound, verifyDeleteResponse.StatusCode);
// }


    [Test] //......check for POST booking authorized to Customer
    public async Task Post_Booking()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

         string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Customer\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        
        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        var booking = new
        {
        NoOfPersons = 2,
        FromDate = DateTime.Now.AddDays(1),
        ToDate = DateTime.Now.AddDays(3),
        Status = "Pending",
        TotalPrice = 200.0,
        Address = "Test Address",
        UserId = 1, 
        };

        string requestBody = JsonConvert.SerializeObject(booking);
        HttpResponseMessage response = await _httpClient.PostAsync("/api/booking", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        
       Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test] //..........Check for GET booking authorized to Admin
    public async Task Backend_TestGetAllBooking()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

         string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        HttpResponseMessage getBookingsResponse = await _httpClient.GetAsync("/api/booking");
        
        Assert.AreEqual(HttpStatusCode.OK, getBookingsResponse.StatusCode);
    }
// [Test]
// public async Task GET_BookingByBookingId()
// {
//     // Register a customer user
//     string uniqueId = Guid.NewGuid().ToString();
//     string uniqueUsername = $"abcd_{uniqueId}";
//     string uniquePassword = $"abcdA{uniqueId}@123";
//     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

//     string registerRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
//     HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

//     // Log in as Customer
//     string customerLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
//     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(customerLoginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

//     string responseString = await loginResponse.Content.ReadAsStringAsync();
//     dynamic responseMap = JsonConvert.DeserializeObject(responseString);
//     string customerAuthToken = responseMap.token;

//     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

//     // Make a GET request to get a booking by its booking ID
//     long bookingId = 1; // Set the booking ID to the desired value
//     HttpResponseMessage getBookingByBookingIdResponse = await _httpClient.GetAsync($"/api/booking/{bookingId}");
//     Assert.AreEqual(HttpStatusCode.OK, getBookingByBookingIdResponse.StatusCode);

//     // Deserialize the response content as a booking
//     string responseBody = await getBookingByBookingIdResponse.Content.ReadAsStringAsync();
//     var booking = JsonConvert.DeserializeObject<Booking>(responseBody);
//     Assert.IsNotNull(booking);

//     // Ensure that the retrieved booking has the correct ID
//     Assert.AreEqual(bookingId, booking.BookingId);
// }

    [Test] // .............Check GET booking by UserId
    public async Task GET_BookingByUserId()
    {
        // Register a customer user
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string registerRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Log in as Customer
        string customerLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(customerLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string customerAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

        // Make a GET request to get bookings by user ID
        HttpResponseMessage getBookingByUserIdResponse = await _httpClient.GetAsync($"/api/booking/{responseMap.userId}"); // Assuming bookingId is available in responseMap
        Assert.AreEqual(HttpStatusCode.OK, getBookingByUserIdResponse.StatusCode);

        // Deserialize the response content as a list of bookings
        string responseBody = await getBookingByUserIdResponse.Content.ReadAsStringAsync();
        var bookings = JsonConvert.DeserializeObject<List<Booking>>(responseBody);

        // Assert that the returned bookings list is not null and contains at least one booking
        Assert.IsNotNull(bookings);
    }
  [Test] //............check for DELETE booking 
    public async Task DeleteBooking()
    {
        // Arrange
        long bookingIdToDelete = 1; // Provide the booking ID to delete
        var uniqueId = Guid.NewGuid().ToString();
        var uniqueUsername = $"abcd_{uniqueId}";
        var uniquePassword = $"abcdA{uniqueId}@123";
        var uniqueEmail = $"abcd{uniqueId}@gmail.com";

        // Register a customer
        var registerRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Customer\"}}";
        var registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Log in as customer
        var loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquePassword}\"}}";
        var loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        var responseMap = JsonConvert.DeserializeObject<Dictionary<string, object>>(loginResponseBody);
        var authToken = responseMap["token"].ToString();

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

        // Add a booking
        var booking = new
        {
            NoOfPersons = 2,
            FromDate = DateTime.Now.AddDays(1),
            ToDate = DateTime.Now.AddDays(3),
            Status = "Pending",
            TotalPrice = 200.0,
            Address = "Test Address",
            UserId = 1 // Assuming UserId for the logged-in user
        };

        var requestBody = JsonConvert.SerializeObject(booking);
        var addBookingResponse = await _httpClient.PostAsync("/api/booking", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, addBookingResponse.StatusCode);

        // Act: Delete the booking
        var deleteBookingResponse = await _httpClient.DeleteAsync($"/api/booking/{bookingIdToDelete}");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, deleteBookingResponse.StatusCode);

        // Verify that the booking is deleted
        var verifyDeleteResponse = await _httpClient.GetAsync($"/api/booking/{bookingIdToDelete}");
        Assert.AreEqual(HttpStatusCode.NotFound, verifyDeleteResponse.StatusCode);
    }


    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }
}
