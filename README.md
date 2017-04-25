# sysPass.NET
**sysPass.NET** is a C# Library allowing access to the sysPass ( https://syspass.org ) API which is incredible easy to use thanks to C# dynamic objects.

NOT PRODUCTION READY

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
((dynamic)parameters).name = "testdescr";
responseObj = sysPassConn.objectResponse("addCustomer", parameters);
```

Example using sysPass.NET from Powershell:
```

```

> **Note:**

> - sysPass.NET uses Newtonsoft.Json library for json serialization and deserialization
> - .NET 4.0, but you can compile to any version of .NET as long as it supports ExpandoObject and Newtonsoft.Json 