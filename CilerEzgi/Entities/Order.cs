using CilerEzgi.Data;

public class Order
{

    public int Id { get; set; }
    public string Price { get; set; }
    public string Fullname { get; set; }
    public string Phone { get; set; }
    public string TcId { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public int PricingName { get; set; } // alınan paketin adı 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string OrderNumber { get; set; }
    public bool IsPay { get; set; } = false;

}