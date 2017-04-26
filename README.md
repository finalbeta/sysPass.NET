# sysPass.NET
**sysPass.NET** is a C# Library allowing access to the sysPass ( https://syspass.org ) API which is incredible easy to use thanks to C# dynamic objects.

Example (List customers with filter and create a new customer:
```
sysPass sysPassConn = new sysPass("http://sysPass.tld/sysPass/api.php", "ecbacg9d9c65c957a7h10d83dfe675e8ef27a912af6b4bcbaa5g0bcd8ie7b4aa", null, null);
ExpandoObject parameters = new ExpandoObject();
((dynamic)parameters).name = "test";
dynamic responseObj = sysPassConn.objectResponse("getCustomers", parameters);
foreach (var item in responseObj.result)
{
    if (item.Key.Equals("count", StringComparison.Ordinal))
    {
        Console.WriteLine("Found a total of {0} customers", item.Value);
    }else
    {
        Console.WriteLine("Found customer {0} with description {1}", item.Value.customer_name, item.Value.customer_description);
    }
}
parameters = new ExpandoObject();
((dynamic)parameters).name = "testname";
((dynamic)parameters).description = "testdescr";
responseObj = sysPassConn.objectResponse("addCustomer", parameters);
```

Example using sysPass.NET from Powershell:
```
[Reflection.Assembly]::LoadFile("C:\pathtodll\Newtonsoft.Json.dll");
[Reflection.Assembly]::LoadFile("C:\pathtodll\sysPassApi.dll");
$sysPassConn = New-Object "sysPassApi.syspass" ("http://sysPass.tld/sysPass/api.php", "ecbacg9d9c65c957a7h10d83dfe675e8ef27a912af6b4bcbaa5g0bcd8ie7b4aa", [NullString]::Value, [NullString]::Value);
$params=[System.Dynamic.ExpandoObject]::new();
$responseObj = $sysPassConn.objectResponse("getCustomers", $params)
foreach ( $data in $responseObj.result) {
    foreach ( $item in $data) {
        if($item.Key -ne "count"){
            Write-Output "Found customer $($item.value.customer_name) with description $($item.value.customer_description)"
        }else{
            Write-Verbose "Returned $($item.value) results"
        }
    }
}
$params=[System.Dynamic.ExpandoObject]::new();
$params.name = "Testname"
$params.description = "Some new customer"
$responseObj = $sysPassConn.objectResponse("addCustomer", $params)
```

> **Note:**

> - sysPass.NET uses Newtonsoft.Json library for json serialization and deserialization
> - .NET 4.0, but you can compile to any version of .NET as long as it supports ExpandoObject and Newtonsoft.Json 
> - If you want to use basic authorization, add the username/password as the last two parameters for the constructor.