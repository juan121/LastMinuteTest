Project Title
Test for the interview process of LastMinute in order to complete it according to the guideline.

Getting Started
To Run the project, download it from Github repository https://github.com/juan121/LastMinuteTest or Azure DevOps URL https://juangit.visualstudio.com/LastMinuteTest, select Master branch and click on "Clone or download".
Once it is downloades, open it with Visual Studio Comunity 2019 and execute main project "SalesTaxes". It will read three json files with the input data 
described in the test and will show the output coming from the console application.

Once you make some changes on the project, the approach follow is to commit and push in the developmentBranch and then create a PR from here to MasterBranch. Build in Azure DevOps will start automatically and you 
can see it in https://juangit.visualstudio.com/LastMinuteTest/_build

Prerequisites
VisualStudio 2019, Microsoft .Net FrameWork

Code design and OOP concepts
Utilities project
Model folder
Data folder
DI .Net Core 3.1
DI ServiceProvider
Interfaces and classes
Inheritance and polymorphism
Static classes
Virtual and Override
Arrow functions
LinQ
Software Design Patterns (Singleton)
Generic T methods
Exception treatment

Running the tests
To run the tests, go to the test project SalesTaxes.Tests and run the battery tests. Notice they are organized in three categories.

Break down into end to end tests
Tests have been developed following TDD approach, so they have been implement before implementing the main functionality of the program. It aims to test actions performed by
the console application in order to ensure their correct outcome. In this particular example I haven't use any mocking framework as I understood it was not necessary
and I thought it was overengineering

And coding style tests
Tests have followed AAA structure using NUnit 3.12 Framework.

Give an example
Deployment
Add additional notes about how to deploy this on a live system

Built With
Dropwizard - The web framework used
Maven - Dependency Management
ROME - Used to generate RSS Feeds
Contributing
Please read CONTRIBUTING.md for details on our code of conduct, and the process for submitting pull requests to us.

Versioning
We use SemVer for versioning. For the versions available, see the tags on this repository.

Authors
Juan Felipe Encinas Pascual

Acknowledgments
https://nunit.org/docs
https://github.com/
https://stackoverflow.com/
https://csharpindepth.com/
https://dzone.com/