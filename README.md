# ExpenseClaim
Expense  claim Application used for calculating the saletax based on the input text passed  to the Api


    >>    This Source Repo Contains ExpenseClaim workable solution and Unit Test Project using Xunit Framework and moq the data in test project



		EndPoint:  api/ExpenseClaim/extract

	Below are the sample  working request  as per the coding excersice , it cover all the scenarios mentioned in the document.


	Posstive Scenario :

	1.  Calculate the Tax 

	Success Cases : 

	1. Sample Input request :

	Input:

	EndPoint:  api/ExpenseClaim/extract

		Request : 

		{
		  "text": "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested…<expense><cost_centre>DEV632</cost_centre><total>790,000</total><payment_method>personal card</payment_method></expense>"
		}


		Response:

		{
		  "error": null,
		  "success": true,
		  "message": "Data extracted and calculated successfully",
		  "data": {
			"costCentre": "DEV632",
			"total": 790000,
			"salesTax": 71818.18,
			"totalExcludingTax": 718181.82,
			"paymentMethod": "personal card"
		  }
		}
  
		  
		  Request : 
		  
		  {
		  "text": "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested…<expense><cost_centre></cost_centre><total>790,000</total><payment_method>personal card</payment_method></expense>"
		}

			Response:
			
			{
		  "error": null,
		  "success": true,
		  "message": "Data extracted and calculated successfully",
		  "data": {
			"costCentre": "UNKNOWN",
			"total": 790000,
			"salesTax": 71818.18,
			"totalExcludingTax": 718181.82,
			"paymentMethod": "personal card"
		  }
		}

		 Request : 
		 
			{
		  "text": "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested…<expense><cost_centre></cost_centre><total>790,000</total></expense>"
		}

		Response :

		   {
		  "error": null,
		  "success": true,
		  "message": "Data extracted and calculated successfully",
		  "data": {
			"costCentre": "UNKNOWN",
			"total": 790000,
			"salesTax": 71818.18,
			"totalExcludingTax": 718181.82,
			"paymentMethod": null
		  }
		}


2. Failure Conditions
    2.1    Opening tags that have no corresponding closing tag. In this case the entire message must be rejected. 
	
			Input Sample:
			
		{
		  "text": "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested…<expense><cost_centre>DEV632</cost_centre><total>35,000</total>>personal card</payment_method></expense>"
		}


		Response : 

		{
		  "error": "Error parsing expense claim: The 'expense' start tag on line 1 position 2 does not match the end tag of 'payment_method'. Line 1, position 80.",
		  "success": false,
		  "message": null,
		  "data": null
		}


  2.2  Missing <total>. In this case the entire message must be rejected. 
		  
				   Input Sample :
				   
					{
		  "text": "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested…<expense><cost_centre>DEV632</cost_centre><payment_method>personal card</payment_method></expense>"
				}

			Response:
			
					{
				  "error": "Error parsing expense claim: Missing required field: <total>",
				  "success": false,
				  "message": null,
				  "data": null
					}
					
					
					 Input Sample :
					 {
			   "text": "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested…<expense><cost_centre>DEV632</cost_centre><total></total><payment_method>personal card</payment_method></expense>"
					}
				  Response :

				{
				  "error": "Error parsing expense claim: Missing required fields in XML (Total is Missing)",
				  "success": false,
				  "message": null,
				  "data": null
				}
				
		
		2.3 Missing <cost_centre>. In this case value of the ‘cost centre’ field in the output must default to ‘UNKNOWN’.
		
				
				Input Request :
				
				
				{
		  "text": "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested…<expense><total>35,000</total><payment_method>personal card</payment_method></expense>"
			   }
			   
			   
			   Response:
			   
					   {
				  "error": null,
				  "success": true,
				  "message": "Data extracted and calculated successfully",
				  "data": {
					"costCentre": "UNKNOWN",
					"total": 35000,
					"salesTax": 3181.82,
					"totalExcludingTax": 31818.18,
					"paymentMethod": "personal card"
					 }
				  }
				  
				  
				  
			Common / Unsual Error Case:

Error Case  1
				  Input:

				  {
					"text": "Hi Patricia, Please create an expense claim for the below.
				   }


			Response:

			{
			  "error": "Error parsing expense claim: Invalid XML content: Opening tag without corresponding closing tag/ Message is not in Required format ",
			  "success": false,
			  "message": null,
			  "data": null
			}		  
			
	
Error Case  2					
			    Input :
					   
				{
				  "text": ""
				}



				Response : 

				{
				  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
				  "title": "One or more validation errors occurred.",
				  "status": 400,
				  "errors": {
					"Text": [
					  "The Text field is required."
					]
				  },
				  "traceId": "00-a85388938cc43a85b8350544062bd29b-cf3a4060a228972d-00"
				}