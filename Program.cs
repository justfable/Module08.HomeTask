using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module08.HomeTask
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ////1
            //SqrArray sqrarr = new SqrArray(5);
            //sqrarr[0] = 11;
            //sqrarr[1] = 5;
            //sqrarr[2] = 8;
            //sqrarr[4] = 3;
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine(i + " элемент = " + sqrarr[i]);
            //}




            ////2

            // Устанавливаем тарифы и льготы
            Tariff baseTariff = new Tariff
            {
                HeatingRate = 5.0,
                WaterRate = 2.0,
                GasRate = 3.0,
                RepairRate = 1.0
            };
            Discount baseDiscount = new Discount
            {
                LaborVeteran = 30.0,
                WarVeteran = 50.0
            };

            // Задаем параметры помещения
            double area = 100.0;
            int occupants = 4;
            bool isWinter = true;

            // Создаем калькулятор коммунальных платежей
            UtilityCalculator calculator = new UtilityCalculator(baseTariff, baseDiscount, area, occupants, isWinter);
            // Получаем таблицу с платежами
            List<PaymentItem> payments = calculator.CalculatePayments();
            // Выводим таблицу
            Console.WriteLine("Вид платежа\tНачислено\tЛьготная скидка\tИтого");
            foreach (PaymentItem payment in payments)
            {
                Console.WriteLine($"{payment.PaymentType}\t\t{payment.Charged}\t\t{payment.Discount}\t\t{payment.Total}");
            }
            // Рассчитываем и выводим итоговую сумму
            double totalAmount = payments.Sum(payment => payment.Total);
            Console.WriteLine($"\nИтого: {totalAmount}");
            Console.ReadLine();
        }


        //1. Создать индексатор, для одномерного массива, который при установке значения 
        // будет возводить в квадрат передаваемое значение переменной и устанавливать его для 
        // указанного индекса.При получении элемента массива по индексу будет
        // возвращаться его текущее значение.


        //class SqrArray
        //{
        //    public int[] array;

        //    public SqrArray(int size)
        //    {
        //        array = new int[size];
        //    }

        //    // Индексатор
        //    public int this[int index]
        //    {
        //        get
        //        {
        //            return array[index];
        //        }
        //        set
        //        {
        //            array[index] = value * value; 
        //        }
        //    }
        //}





        //2. Написать программу, рассчитывающую сумму коммунальных платежей:
        //есть базовые тарифы на отопление (на 1 м2 площади), на воду (на 1 чел),
        //на газ (на 1 чел), на текущий ремонт (на 1 м2 площади). Задается метраж помещения,
        //количество проживающих людей, сезон (осенью и зимой отопление дороже), наличие льгот
        //(ветеран труда– 30 % от его части; ветеран войны- 50% от его части). Вывести таблицу со
        //столбцами: Вид платежа, Начислено, Льготная скидка, Итого. Посчитать итоговую сумму


        class Tariff
        {
            public double HeatingRate { get; set; }  // Тариф на отопление за 1 м2 площади
            public double WaterRate { get; set; }    // Тариф на воду за 1 человека
            public double GasRate { get; set; }      // Тариф на газ за 1 человека
            public double RepairRate { get; set; }   // Тариф на текущий ремонт за 1 м2 площади
        }

        class Discount
        {
            public double LaborVeteran { get; set; } // Льгота для ветерана труда (в процентах)
            public double WarVeteran { get; set; }   // Льгота для ветерана войны (в процентах)
        }

        class PaymentItem
        {
            public string PaymentType { get; set; }
            public double Charged { get; set; }
            public double Discount { get; set; }
            public double Total { get; set; }
        }

        class UtilityCalculator
        {
            private Tariff tariff;
            private Discount discount;
            private double area;
            private int occupants;
            private bool isWinter;

            public UtilityCalculator(Tariff tariff, Discount discount, double area, int occupants, bool isWinter)
            {
                this.tariff = tariff;
                this.discount = discount;
                this.area = area;
                this.occupants = occupants;
                this.isWinter = isWinter;
            }

            public List<PaymentItem> CalculatePayments()
            {
                List<PaymentItem> paymentItems = new List<PaymentItem>();
                // Рассчитываем отопление
                double heatingCharge = tariff.HeatingRate * area;
                double heatingDiscount = discount.LaborVeteran / 100 * heatingCharge;
                double heatingTotal = heatingCharge - heatingDiscount;

                paymentItems.Add(new PaymentItem
                {
                    PaymentType = "Отопление",
                    Charged = heatingCharge,
                    Discount = heatingDiscount,
                    Total = heatingTotal
                });

                // Рассчитываем воду
                double waterCharge = tariff.WaterRate * occupants;
                double waterDiscount = discount.LaborVeteran / 100 * waterCharge;
                double waterTotal = waterCharge - waterDiscount;

                paymentItems.Add(new PaymentItem
                {
                    PaymentType = "Вода",
                    Charged = waterCharge,
                    Discount = waterDiscount,
                    Total = waterTotal
                });

                // Рассчитываем газ
                double gasCharge = tariff.GasRate * occupants;
                double gasDiscount = discount.LaborVeteran / 100 * gasCharge;
                double gasTotal = gasCharge - gasDiscount;
                paymentItems.Add(new PaymentItem
                {
                    PaymentType = "Газ",
                    Charged = gasCharge,
                    Discount = gasDiscount,
                    Total = gasTotal
                });

                // Рассчитываем текущий ремонт
                double repairCharge = tariff.RepairRate * area;
                double repairDiscount = discount.LaborVeteran / 100 * repairCharge;
                double repairTotal = repairCharge - repairDiscount;

                paymentItems.Add(new PaymentItem
                {
                    PaymentType = "Текущий ремонт",
                    Charged = repairCharge,
                    Discount = repairDiscount,
                    Total = repairTotal
                });

                return paymentItems;
            }
        }
    }
}
