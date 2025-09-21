using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.IO;
using System.Drawing;

namespace BichinhoVirtual
{
    internal class Program
    {

        public const int INCREMENTO = 30;
        public const int DECREMENTO = 40;
        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            // Cria um novo Bitmap com a largura e altura desejadas
            Bitmap resizedImage = new Bitmap(width, height);

            // Desenha a imagem original no novo Bitmap usando as dimensões desejadas
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static string ConvertToAscii(Bitmap image)
        {
            // Caracteres ASCII usados para representar a imagem
            char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '@' };

            StringBuilder asciiArt = new StringBuilder();

            // Percorre os pixels da imagem e converte cada um em um caractere ASCII correspondente
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int asciiIndex = grayScale * (asciiChars.Length - 1) / 255;
                    char asciiChar = asciiChars[asciiIndex];
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

            return asciiArt.ToString();
        }

        static void ExibirImagem(string imagePath, int width, int height)
        {
            // Caminho para a imagem que deseja exibir
            //string imagePath = @"C:\Users\Danilo Filitto\Downloads\Panda.jpg";

            // Carrega a imagem
            Bitmap image = new Bitmap(imagePath);

            // Redimensiona a imagem para a largura e altura desejadas
            int consoleWidth = width;
            int consoleHeight = height;
            Bitmap resizedImage = ResizeImage(image, consoleWidth, consoleHeight);

            // Converte a imagem em texto ASCII
            string asciiArt = ConvertToAscii(resizedImage);

            // Exibe o texto ASCII no console
            Console.WriteLine(asciiArt);


        }

        static void LerArquivoBichinho(string nome, string tutor, ref float alimentado, ref float feliz, ref float limpo)
        {
            string path = Environment.CurrentDirectory + "\\";
            Console.Write("Que bom que voltou! Estava com saudades de você, {0}!!", tutor);
            string arq = path + nome + tutor + ".txt";
            if (File.Exists(arq))
            {
                string[] dados = File.ReadAllLines(arq);
                alimentado = float.Parse(dados[2]);
                limpo = float.Parse(dados[3]);
                feliz = float.Parse(dados[4]);
                if (alimentado < 0 || limpo < 0 || feliz < 0)
                {
                    Console.WriteLine("ASSISTENTE VIRTUAL:");
                    Console.WriteLine("O seu bichinho foi desativado!");
                    Console.WriteLine("Vamos então reativar o seu bichinho!");
                    Thread.Sleep(2000);
                    Console.WriteLine("Pronto! Ele está novamente saudável, limpo e feliz");
                    Console.WriteLine("Pressione qualquer tecla para continuar!");
                    alimentado = 100;
                    limpo = 100;
                    feliz = 100;
                    Console.ReadKey();
                }
            }
        }

        static void GravarArquivoBichinho(string nome, string tutor, float alimentado, float feliz, float limpo)
        {
            string path = Environment.CurrentDirectory + "\\";
            string arq = path + nome + tutor + ".txt";
            string fileContent = nome + Environment.NewLine;
            fileContent += tutor + Environment.NewLine;
            fileContent += alimentado + Environment.NewLine;
            fileContent += limpo + Environment.NewLine;
            fileContent += feliz + Environment.NewLine;
            File.WriteAllText(arq, fileContent);
        }

        static string Falar()
        {
            string[] frases = new string[4];
            frases[0] = "Cadê você, filho da puta?";
            frases[1] = "Não fiz nada hoje, queria muito passear";
            frases[2] = "Demorou hoje por que?";
            frases[3] = "Muito trabalho e pouca diversão, fazem de mim um bobão";
            Random rand = new Random();
            return frases[rand.Next(frases.Length)];
        }

        static void AtualizarStatus(ref float alimentado, ref float limpo, ref float feliz)
        {
            Random rand = new Random();
            int caracteristica = rand.Next(3);
            switch (caracteristica)
            {
                case 0:
                    alimentado -= rand.Next(DECREMENTO);
                    break;
                case 1:
                    limpo -= rand.Next(DECREMENTO);
                    break;
                case 2:
                    feliz -= rand.Next(DECREMENTO);
                    break;
            }
        }

        static string Interagir(string tutor, ref float alimentado, ref float limpo, ref float feliz)
        {
            Console.WriteLine("Brincar ?/Comer ?/Banho ?/ Nada?:");
            string entrada = Console.ReadLine().ToLower();
            Random rand = new Random();
            switch (entrada)
            {
                case "comer":
                    alimentado += rand.Next(INCREMENTO);
                    break;
                case "banho":
                    limpo += rand.Next(INCREMENTO);
                    break;
                case "brincar":
                    feliz += rand.Next(INCREMENTO);
                    break;

            }
            if (alimentado > 100) alimentado = 100;
            if (limpo > 100) limpo = 100;
            if (feliz > 100) feliz = 100;
            return entrada;
        }

        static void ExibirStatus(string tutor, float alimentado, float limpo, float feliz, int tipo)
        {
            if (tipo == 0)
            {
                Console.WriteLine("Status do meu bichinho");
                Console.WriteLine("Alimentado: [0]", alimentado);
                Console.WriteLine("Limpo: [0]", limpo);
                Console.WriteLine("Feliz: [0]", feliz);

            }
            if (tipo == 1)
            {
                if (alimentado > 40 && alimentado < 60)
                {
                    Console.WriteLine("Tô com fome, porra!");
                    Console.WriteLine("Vai tazer o rango ou não?");
                }
                if (limpo > 40 && limpo < 60)
                {
                    Console.WriteLine("Tô sujo, porra!");
                    Console.WriteLine("Vai me dar banho ou vou ficar fedendo aqui?");
                }
                if (feliz > 40 && feliz < 60)
                {
                    Console.WriteLine("Tô triste, porra!");
                    Console.WriteLine("Me dá atenção, porra!");
                }

            }
            if (tipo == 2)
            {
                Console.WriteLine("Status do meu bichinho");
                Console.WriteLine("Alimentado: [0]", alimentado);
                Console.WriteLine("Limpo: [0]", limpo);
                Console.WriteLine("Feliz: [0]", feliz);
                if (alimentado > 40 && alimentado < 60)
                {
                    Console.WriteLine("Tô com fome, porra!");
                    Console.WriteLine("Vai tazer o rango ou não?");
                }
                if (limpo > 40 && limpo < 60)
                {
                    Console.WriteLine("Tô sujo, porra!");
                    Console.WriteLine("Vai me dar banho ou vou ficar fedendo aqui?");
                }
                if (feliz > 40 && feliz < 60)
                {
                    Console.WriteLine("Tô triste, porra!");
                    Console.WriteLine("Me dá atenção, porra!");
                }
            }
        }

        static void LerDados(ref string nome, ref string tutor) 
        {
            Console.WriteLine("Qual é o seu nome?");
            tutor = Console.ReadLine();
            Console.WriteLine("Qual o nome do seu bichinho virtual");
            nome = Console.ReadLine();
            Console.WriteLine("Olá, {0}! Sou seu bichinho virtual", tutor);
        }


        static void Main(string[] args)
        {
            string entrada = "";
            string foto = Environment.CurrentDirectory + "\\foto.jpg";
            string nome = "";
            string tutor = "";
            float alimentado = 100;
            float limpo = 100;
            float feliz = 100;                     
            ExibirImagem(foto, 35, 25);
            Console.WriteLine("MEU BICHINHO VIRTUAL");
            LerDados(ref nome, ref tutor);          
            LerArquivoBichinho(nome, tutor, ref alimentado, ref limpo, ref feliz);
            while (entrada.ToLower() != "nada" && alimentado > 0 && limpo > 0 && feliz > 0)
            {
                Console.Clear();                
                Console.WriteLine(Falar());
                Console.WriteLine("O que vamos fazer hoje?");
                Thread.Sleep(3000);                
         
                // Status do bichinho : 0 - Alimento; 1 - Limpo; 2 - Feliz
                AtualizarStatus(ref alimentado, ref limpo, ref feliz);
                Console.Clear();                             
                ExibirStatus(tutor, alimentado, limpo, feliz, 1);
                Thread.Sleep(1000);
                Console.Clear();
                entrada = Interagir(tutor, ref alimentado, ref limpo, ref feliz);               
            }
            Console.Clear();
            if (alimentado < 0 || limpo < 0 || feliz < 0)
            {
                Console.WriteLine("Estou muito fraco...");
                Console.WriteLine("Vou para o hospital...");
                Console.WriteLine("Peça ao Assistente Virtual me trazer de volta...");
                
            }
            else
            {
                Console.Write("Obrigado por cuidar de mim!");
                Console.Write("Tchaaaaau!");
            }
            GravarArquivoBichinho (nome, tutor, alimentado, feliz, limpo);
            Console.ReadKey();
        }
    }
}

