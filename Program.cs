using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*Esses daqui em baixo são end points que são partes do codigo
usados para encaminhar até o final*/
app.MapGet("/", () => "Hello World 2");
/*Aqui o c# ja transforma ele em json*/
app.MapPost("/user", ()=> new {Name="Stephany Batista", Age=35});
app.MapGet("/AddHeader", (HttpResponse response)=> {
    response.Headers.Add("Teste","Stephany Batista");
    return "Retorno";
    });

/*Parte de Post da aplicação usada para postar dados da aplicação*/
app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

/*parte get que pega da aplicação*/
//api.app.com/users?datastart={date}&dateend={date}
app.MapGet("/getproduct", ([FromQuery] string dateStart,[FromQuery] string dateEnd)=>{
    return dateStart + " - "+ dateEnd;
});

/*get que é usado para pegar um dado especifico, ele retorna um do tipo produto*/
//api.app.com/users/{code}
app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});

/*get que pega dadods direto do cabeçalho*/
//api.app.com/users?datastart={date}&dateend={date}
app.MapGet("/getproductbyheader", (HttpRequest request)=>{
    return request.Headers["product-code"].ToString();
});

/*metodo usado para fazer o put, mudar o dado de um determinado numero*/
app.MapPut("/editproduct", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

/*Delete usado para deletar um dados*/
app.MapDelete("/deleteproduct/{code}",([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
});

app.Run();

public static class ProductRepository{ 
    public static List<Product> Products {get;set;}

    public static void Add(Product product){
        if (Products == null)
            Products = new List<Product>();
        Products.Add(product);
    }
    public static Product GetBy(string code){
        return Products.FirstOrDefault(p => p.Code == code);
    }
    public static void Remove(Product product){
        Products.Remove(product);
    }
}

public class Product{
    public string Code { get; set;}
    public string Name { get; set;}
}

    




