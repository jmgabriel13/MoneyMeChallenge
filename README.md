# Quote Calculator

## Clone

```shell
git clone https://github.com/jmgabriel13/MoneyMeChallenge.git
cd MoneyMeChallenge/
```

## Set Up
- ConnectionStrings
    - Open the solution(MoneyMeChallenge.sln) with Visual Studio and navigate to appsettings.json from <b>WebApp</b> project, the current ConnectionString is on default instance of <b>SQLEXPRESS</b>, change the <b>Data Source</b> based on your default instance.
- Set start up Project
    - Right Click <b>WebApp</b> from solution explorer and set it as Start up Project.
- Database Migration
    - Open Package Manager Console then run databse migration using ef core.
    ```shell
        update-database
    ```
    - If the result is error, make sure the default project in Package Manager Console is <b>Infrastructure</b> and you set the <b>WebApp<b> Start up Project.
 
## Front End
- Front-end link
    - The front end default link is on <b>"/"</b>, or <b>/quote-calculation</b>
- Change API_BASE_URL
  - Navigate to <b>ClientApp/LoanApplication/</b>, edit the <b>config.ts</b> based on the url local port of <b>WebApp</b>.
  - After you change the API_BASE_URL from front end, run build from <b>ClientApp/LoanApplication/</b>
  ```shell
        npm run build
  ```
  - Then the build files will be located at wwwroot of <b>WebApp</b> project and served it as static files.


## API Endpoint

## Customer Loan Rate

Used to get customer loan rate, that accept the following request

**URL** : `/api/customers/loan/rate/`

**Method** : `POST`

**Auth required** : NO

**Data constraints**

```json
{
  "amountRequired": "string",
  "term": "string",
  "title": "string",
  "firstName": "string",
  "lastName": "string",
  "dateOfBirth": "string",
  "mobile": "string",
  "email": "string"
}
```

**Data example**

```json
{
  "amountRequired": "5000",
  "term": "2",
  "title": "Mr.",
  "firstName": "Johny",
  "lastName": "doe",
  "dateOfBirth": "1900-01-01",
  "mobile": "0422111333",
  "email": "layton@test.com"
}
```

## Success Response

**Code** : `200 OK`

**Content example**

```json
{
    "value": "https://localhost:7089/quote-calculator?customerId=182c4852-3876-4deb-97d6-fee1057d8baa",
    "isSuccess": true,
    "isFailure": false,
    "error": {
        "code": "",
        "message": ""
    }
}
```
**Here you can copy the result value then paste it to the browser to access the front end with your data**

## Error Response

**Condition** : If 'dateOfbirth' is not valid date time value.

**Code** : `400 BAD REQUEST`

**Content** :

```json
{
    "type": "Customer.DateOfBirth",
    "title": "Bad Request",
    "status": 400,
    "detail": "Date of Birth is not valid.",
    "errors": [
        ""
    ]
}
```
    
