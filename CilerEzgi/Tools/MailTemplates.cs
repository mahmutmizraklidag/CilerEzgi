namespace CilerEzgi.Tools
{
    public class MailTemplates
    {
        public static string CustomerTemplate(Order order)
        {
            string customerMailTemplate = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Siparişiniz Onaylandı</title>
    <style>
        body {{ margin: 0; padding: 0; background-color: #f4f7ff; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; }}
        .container {{ width: 100%; max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 20px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.05); margin-top: 20px; margin-bottom: 20px; }}
        .header {{ background-color: #d8947c; padding: 40px 20px; text-align: center; }}
        .header h1 {{ color: #ffffff; margin: 0; font-size: 24px; font-weight: 600; letter-spacing: 1px; }}
        .content {{ padding: 40px 30px; color: #6b6b6b; }}
        .greeting {{ font-size: 18px; color: #242424; margin-bottom: 20px; font-weight: 600; }}
        .order-box {{ background-color: #f9f9f9; border: 1px solid #eee; border-radius: 15px; padding: 20px; margin: 20px 0; }}
        .order-row {{ display: flex; justify-content: space-between; margin-bottom: 10px; border-bottom: 1px solid #eee; padding-bottom: 10px; }}
        .order-row:last-child {{ border-bottom: none; margin-bottom: 0; padding-bottom: 0; }}
        .label {{ font-weight: 600; color: #242424; }}
        .footer {{ background-color: #242424; color: #ffffff; text-align: center; padding: 20px; font-size: 12px; }}
        .btn {{ display: inline-block; background-color: #d8947c; color: #ffffff; padding: 12px 30px; text-decoration: none; border-radius: 25px; margin-top: 20px; font-weight: bold; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Siparişiniz Onaylandı! 🎉</h1>
        </div>
        <div class='content'>
            <p class='greeting'>Merhaba {order.Fullname},</p>
            <p>Aramıza hoş geldin! Sipariş ödemen başarıyla alındı ve kaydın oluşturuldu.</p>
            
            <div class='order-box'>
                <div class='order-row'>
                    <span class='label'>Sipariş No:</span>
                    <span>#{order.OrderNumber}</span>
                </div>
                <div class='order-row'>
                    <span class='label'>Paket:</span>
                    <span>{order.ParentPricingName + order.PricingName}</span> </div>
                <div class='order-row'>
                    <span class='label'>Tutar:</span>
                    <span style='color:#d8947c; font-weight:bold;'>{order.Price} ₺</span>
                </div>
                <div class='order-row'>
                    <span class='label'>Tarih:</span>
                    <span>{DateTime.Now.ToString("dd.MM.yyyy HH:mm")}</span>
                </div>
            </div>

            <p>Sürecinle ilgili detaylar için seninle en kısa sürede iletişime geçeceğiz. Herhangi bir sorun olursa bu maili yanıtlayabilirsin.</p>
            
            <div style='text-align:center;'>
                <a href='https://seninsiten.com' class='btn'>Web Sitesine Git</a>
            </div>
        </div>
        <div class='footer'>
            <p>© {DateTime.Now.Year} Çiler Ezgi. Tüm Hakları Saklıdır.</p>
        </div>
    </div>
</body>
</html>";
            return customerMailTemplate;

        }
        public static string AdminTemplate(Order order)
        {
            string adminMailTemplate = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Yeni Sipariş Bildirimi</title>
    <style>
        body {{ margin: 0; padding: 0; background-color: #f4f7ff; font-family: Arial, sans-serif; }}
        .container {{ width: 100%; max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 10px; overflow: hidden; border-top: 5px solid #242424; margin-top:20px; }}
        .header {{ padding: 20px; background-color: #f8f9fa; border-bottom: 1px solid #eee; }}
        .header h2 {{ margin: 0; color: #242424; font-size: 20px; }}
        .content {{ padding: 30px; }}
        .info-group {{ margin-bottom: 20px; }}
        .label {{ display: block; font-size: 12px; color: #999; text-transform: uppercase; letter-spacing: 1px; margin-bottom: 5px; }}
        .value {{ display: block; font-size: 16px; color: #333; font-weight: 500; border-bottom: 1px solid #eee; padding-bottom: 10px; }}
        .price-badge {{ background-color: #d8947c; color: white; padding: 5px 10px; border-radius: 5px; font-weight: bold; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h2>🔔 Yeni Bir Satış Gerçekleşti!</h2>
        </div>
        <div class='content'>
            <div class='info-group'>
                <span class='label'>Müşteri Adı Soyadı</span>
                <span class='value'>{order.Fullname}</span>
            </div>
            <div class='info-group'>
                <span class='label'>Telefon</span>
                <span class='value'><a href='tel:{order.Phone}' style='color:#333; text-decoration:none;'>{order.Phone}</a></span>
            </div>
            <div class='info-group'>
                <span class='label'>E-Posta</span>
                <span class='value'><a href='mailto:{order.Email}' style='color:#333; text-decoration:none;'>{order.Email}</a></span>
            </div>
            <div class='info-group'>
                <span class='label'>Satın Alınan Paket</span>
                <span class='value'>{order.ParentPricingName+" "+order.PricingName}</span>
            </div>
            <div class='info-group'>
                <span class='label'>Ödenen Tutar</span>
                <span class='value'><span class='price-badge'>{order.Price} ₺</span></span>
            </div>
            <div class='info-group'>
                <span class='label'>Adres</span>
                <span class='value'>{order.Address} <br> {order.District} / {order.City}</span>
            </div>
            <div class='info-group'>
                <span class='label'>Sipariş Numarası (PayTR)</span>
                <span class='value'>{order.OrderNumber}</span>
            </div>
        </div>
    </div>
</body>
</html>";
            return adminMailTemplate;
        }
    }
}
