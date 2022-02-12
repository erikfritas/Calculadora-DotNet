using System;

namespace Calculator{
    class Program {
        delegate int Del(string[] nums);

        static void Main(string[] args){
            Console.Clear();
            Console.WriteLine("Iniciando Calculadora...");
            var ins = delegate(char s) {
                return "\nInsira o valor separado por sinais ex: 5"+s+"1"+s+"2"+s+"4";
            };

            var sinais = new char[] {
                '+', '-', '*', '/'
            };

            string historico = "";

            var optNum = -1;
            do {
                var opt = Menu(historico);
                if (opt != null){
                    try {
                        optNum = int.Parse(opt);
                    } catch (System.Exception) { Console.Clear(); OptError(); continue; }

                    if (optNum >= 0 && optNum < sinais.Length){
                        Console.WriteLine(ins(sinais[optNum]));
                        var read = Console.ReadLine();
                        var nums = read?.Split(sinais[optNum]);
                        if (nums != null){
                            try {
                                var result = GetOpt(optNum, nums);
                                Console.WriteLine("\nE o resultado é: "+result);
                                historico += read+" = "+result+"\n";
                            } catch (System.Exception) {
                                Console.WriteLine("Expressão inválida para esta opção...\n");
                            }
                        } else Console.WriteLine("\nO sinal de "+ sinais[optNum] +" não encontrado na expressão.");
                    } else OptError();
                } else OptError();

                Console.Write("\nDeseja continuar? (S/N) ");
                if (Console.ReadLine()?.ToUpper() == "N") {
                    Console.WriteLine("\nResultado final:\n {"+historico+"\n}");
                    Console.WriteLine("\n\nCriado por Érik Freitas");
                    break;
                } else {
                    Console.Clear();
                    Console.WriteLine("Continuando...\n");
                }
            } while (true);
        }

        static string? Menu(string hist) {
            Console.WriteLine("Histórico:\n"+hist);
            Console.WriteLine("Escolha uma opção: ");
            Console.WriteLine("(0) Soma");
            Console.WriteLine("(1) Subtração");
            Console.WriteLine("(2) Multiplicação");
            Console.Write("(3) Divisão\n-> ");
            return Console.ReadLine();
        }

        static void OptError() {
            Console.WriteLine("\nOpção inexistente...");
        }

        static int GetOpt(int opt, string[] nums) {
            return new Del[4] {
                delegate(string[] nums){ // Soma
                    var num = int.Parse(nums[0]);
                    for (int i = 1; i < nums?.GetLength(0); i++) {
                        num += int.Parse(nums[i]);
                    }
                    
                    return num;
                },
                delegate(string[] nums){ // Subtração
                    var num = int.Parse(nums[0]);
                    for (int i = 1; i < nums?.GetLength(0); i++) {
                        num -= int.Parse(nums[i]);
                    }
                    
                    return num;
                },
                delegate(string[] nums){ // Multiplicação
                    var num = int.Parse(nums[0]);
                    for (int i = 1; i < nums?.GetLength(0); i++) {
                        num *= int.Parse(nums[i]);
                    }
                    
                    return num;
                },
                delegate(string[] nums){ // Divisão
                    var num = int.Parse(nums[0]);
                    for (int i = 1; i < nums?.GetLength(0); i++) {
                        num /= int.Parse(nums[i]);
                    }
                    
                    return num;
                }
            }[opt](nums);
        }
    }
}
