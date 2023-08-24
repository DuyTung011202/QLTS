using System.Collections;

namespace MISA.WebFresher05.Demo
{
    public class Calculator
    {
        public long Add(int x, int y)
        {
            return x + (long)y;
        }

        public int Add(string str)
        {
            try
            {
                if (str == "")
                {
                    return 0;
                }

                string[] values = str.Split(',');

                int total = 0;

                ArrayList negatives = new ArrayList();

                // Duyệt qua mảng các chuỗi con
                foreach (var value in values)
                {
                    if (value.Trim() != "")
                    {
                        if (int.TryParse(value, out int number))
                        {
                            if (number < 0)
                            {
                                negatives.Add(number);
                            }
                            else
                            {
                                total += number;
                            }
                        }
                        else
                        {
                            throw new Exception("Không thể chuyển đổi " + value + " thành số nguyên.");
                        }
                    }
                }

                if (negatives.Count > 0)
                {
                    string joinStr = string.Join(", ", negatives.ToArray());
                    throw new Exception("Không chấp nhận toán tử âm: " + joinStr);
                }

                // Trả về biến sum
                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Ném lại ngoại lệ để nó có thể được bắt trong hàm kiểm tra
            }



        }


        public long Sub(int x, int y)
        {
            return x - (long)y;
        }

        public long Mul(int x, int y)
        {
            return x * (long)y;
        }

        public double Div(int x, int y)
        {
            if (y == 0)
            {
                throw new Exception("Không chia được cho 0");
            }
            return x / (double)y;
        }

    }
}
