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

   
      [Test] //......check for POST Review authorized to Customer
    public async Task Backend_TestPostReviews()
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

       var review = new
        {
            UserId = "1",
            Subject = "Test subject",
            Body = "Test body",
            Rating = 5
        };

        string requestBody = JsonConvert.SerializeObject(review);
        HttpResponseMessage response = await _httpClient.PostAsync("/api/Review", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        
       Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test] //..........Check for GET review authorized to Admin
    public async Task Backend_TestGetAllReviews()
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

        HttpResponseMessage getReviewsResponse = await _httpClient.GetAsync("/api/review");
        
        Assert.AreEqual(HttpStatusCode.OK, getReviewsResponse.StatusCode);
    }

    [Test] //.......check for GET resorts 
    public async Task Backend_TestGet_Resorts()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/resort");
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Test] //......check for POST resort authorized to Admin
    public async Task Backend_TestPostResort()
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
        [Test]
public async Task Backend_TestPutResort()
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
    HttpResponseMessage addResortResponse = await _httpClient.PostAsync("/api/resort", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, addResortResponse.StatusCode);

    // Get the added resort details
    string addResortResponseBody = await addResortResponse.Content.ReadAsStringAsync();
    dynamic addResortResponseMap = JsonConvert.DeserializeObject(addResortResponseBody);

    int? resortId = addResortResponseMap?.resortId;

    if (resortId.HasValue)
    {
        var updatedResortDetails = new
        {
            ResortId = resortId, // Corrected variable name and added ResortId
            ResortName = "Updated Resort Name",
            ResortImageUrl = "updated-image-url",
            ResortLocation = "Updated Location",
            ResortAvailableStatus = "Updated Available Status",
            Price = 200,
            Capacity = 30,
            Description = "Updated Description"
        };

        string updateResortRequestBody = JsonConvert.SerializeObject(updatedResortDetails);
        HttpResponseMessage updateResortResponse = await _httpClient.PutAsync($"/api/resort/{resortId}", new StringContent(updateResortRequestBody, Encoding.UTF8, "application/json"));

        // Assert that the resort is updated successfully
        Assert.AreEqual(HttpStatusCode.OK, updateResortResponse.StatusCode);
    }
    else
    {
        // Log additional information for debugging
        string responseContent = await addResortResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Add Resort Response Content: {responseContent}");

        Assert.Fail("Resort ID is null or not found in the response.");
    }
}



    [Test] //......check for POST booking authorized to Customer
    public async Task Backend_TestPostBooking()
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


    [Test] // .............Check GET booking by UserId
    public async Task Backend_TestGetBookingByUserId()
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
    public async Task Backend_TestDeleteBooking()
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
