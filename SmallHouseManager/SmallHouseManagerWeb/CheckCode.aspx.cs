using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace SmallHouseManagerWeb
{
    public partial class CheckCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string message = GenerateCheckCode();
            createCheckCodeImage(message);
        }
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = null;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                number = random.Next();
                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));
                checkCode += code.ToString();
            }

            Session["checkCode"] = checkCode.ToLower();
            return checkCode;
        }

        //根据验证码产生图像


        private void createCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == string.Empty)
                return;
            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);

                }

                Font font = new Font("Arial", 12, FontStyle.Bold | FontStyle.Italic);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 1, 2);
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));　　//ＣＯＬＯＲ表示要分配给指定像素的颜色;

                }

                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());

            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }

        }
    }
}
