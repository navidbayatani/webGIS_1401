<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
    <head>
        <title>Sum of two values</title>
    </head>
    <body>
        <% 
            p1 = Request.QueryString("parameter1")
            p2 = Request.QueryString("parameter2")
			result = p1 + (p2 + 0)
            //Response.Write(result)
        %>
		<p>
			Result of adding <%= p1 %> and <%= p2 %> : <%= result %>
		</p>

    </body>
</html>
