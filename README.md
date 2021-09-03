#Blog Api has been written on .Net Core 3.1 which is cross platform and can be run on Windows, Mac and multiple distributions of Linux.#

#How to run#
1. Download and install .net runtime from https://dotnet.microsoft.com/download/dotnet/3.1 respectivly to your OS.
2. Navigate to \Blog\bin\Release\netcoreapp3.1\publish and run Blog.exe
3. Open https://localhost:5001/swagger with will automatically navigate into swagger UI.

#Swagger#
Swagger is an Interface Description Language for describing RESTful APIs expressed using JSON.
Swagger UI allows to visualize and interact with the API’s resources without having any of the implementation logic in place. 
It’s automatically generated from the OpenAPI Specification, with the visual documentation. 

#Database#
Sql Lite has been used as a database (Blog.db), which is located in Blog project and seed with 2 users and 2 blog posts.

#Real-time updates#
SignalR library could have been used to achieved the real time updates but unfortunatly I couldn't allocate anymore time for this feature. (https://dotnet.microsoft.com/apps/aspnet/signalr)

#Potential Improvments#
The API is absolutly not Production ready:D The following enhancments could have been done
1. Authentication and Authorisation
2. Data Validations
3. Async calls and lazy loading
4. Unit tests are absolutly missing. 
5. Logging

