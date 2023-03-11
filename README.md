# SolisApiTest

I made a quick project for connecting to SolisCloud API.

Big thank you to https://github.com/hultenvp/solis-sensor for a nice example to use while figuring out how to connect with C#

How to get access:
------------------
[SolisCloud](https://www.soliscloud.com/) is the next generation Portal for Solis branded PV systems from Ginlong. It's unknown to me if the other brands are also supported.

The new portal requires a key-id, secret and username to function. You can obtain key and secret via SolisCloud.

Submit a [service ticket](https://solis-service.solisinverters.com/support/solutions/articles/44002212561-api-access-soliscloud) and wait till it is resolved.
Go to https://www.soliscloud.com/#/apiManage.
Activate API management and agree with the usage conditions.
After activation, click on view key tot get a pop-up window asking for the verification code.
First click on "Verification code" after which you get an image with 2 puzzle pieces, which you need to overlap each other using the slider below.
After that, you will receive an email with the verification code you need to enter (within 60 seconds).
Once confirmed, you get the API ID, secret and API URL
