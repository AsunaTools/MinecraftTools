using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace MinecraftTools {
    internal static class App {
        private static void Main(string[] args) {
            Console.Clear();
            if (args.Length != 0) {
                if (args[0] == "--java") {
                    if (args.Length == 1) {
                        Console.WriteLine("Invalid Argument!");
                        Environment.Exit(1);
                    } else {
                        Globals.Java = args[1];
                    }
                } else {
                    Console.WriteLine("Invalid Argument!");
                    Environment.Exit(1);
                }
            }
            Console.WriteLine("==========================================================================");
            Thread.Sleep(100);
            Console.WriteLine("       d8888                                     888b     d888  .d8888b.  ");
            Thread.Sleep(100);
            Console.WriteLine("      d88888                                     8888b   d8888 d88P  Y88b ");
            Thread.Sleep(100);
            Console.WriteLine("     d88P888                                     88888b.d88888 888    888 ");
            Thread.Sleep(100);
            Console.WriteLine("    d88P 888 .d8888b  888  888 88888b.   8888b.  888Y88888P888 888        ");
            Thread.Sleep(100);
            Console.WriteLine("   d88P  888 88K      888  888 888 \"88b     \"88b 888 Y888P 888 888        ");
            Thread.Sleep(100);
            Console.WriteLine("  d88P   888 \"Y8888b. 888  888 888  888 .d888888 888  Y8P  888 888    888 ");
            Thread.Sleep(100);
            Console.WriteLine(" d8888888888      X88 Y88b 888 888  888 888  888 888   \"   888 Y88b  d88P ");
            Thread.Sleep(100);
            Console.WriteLine("d88P     888  88888P   \"Y88888 888  888 \"Y888888 888       888  \"Y8888P\"  ");
            Thread.Sleep(100);
            Console.WriteLine("==========================================================================");
            Thread.Sleep(1000);

            if (!CheckJava(Globals.Java)) {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("          Minecraft Tools          ");
                Console.WriteLine("===================================");
                Console.WriteLine();
                Console.WriteLine("¡Java no esta instalado o no esta declarado!");
                Console.WriteLine("¡Usa \"--java [Ruta]\" para seleccionar la ruta de java!");
                Console.WriteLine();
                Console.WriteLine("Presione cualquier tecla para salir.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (Directory.Exists(MinecraftDir())) {
                var showMenu = true;
                while (showMenu) { showMenu = Menu(); }
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("          Minecraft Tools          ");
                Console.WriteLine("===================================");
                Console.WriteLine();
                Console.WriteLine("¡Hecho! Gracias por usar AsunaTools una vez mas,");
                Console.WriteLine("Escrito por Asuna y Ekardo. (Reescrito en C# por Asuna)");
                Console.WriteLine();
                Console.WriteLine("Presione cualquier tecla para salir.");
                Console.ReadLine();
            } else {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("          Minecraft Tools          ");
                Console.WriteLine("===================================");
                Console.WriteLine();
                Console.WriteLine("¡Minecraft no esta instalado!");
                Console.WriteLine();
                Console.WriteLine("Viste https://www.minecraft.net/download");
                Console.WriteLine("para descargar Minecraft.");
                Console.WriteLine();
                Console.WriteLine("Presione cualquier tecla para salir.");
                Console.ReadLine();
            }
        }

        public static string MinecraftDir() {
            var userName = Environment.UserName;
            var minecraftDir = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                minecraftDir = "C:/Users/" + userName + "/AppData/Roaming/.minecraft";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                minecraftDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.minecraft";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                minecraftDir = "/Users/" + userName + "/Library/Application Support/minecraft";
            }
            return minecraftDir;
        }
        
        private static bool Menu() {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("          Minecraft Tools          ");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine("(1) Descargar Mods");
            Console.WriteLine("(2) Instalar Forge (Version del server)");
            Console.WriteLine("(3) Instalar OptiFine (Ultima version)");
            Console.WriteLine();
            Console.WriteLine("(4) Salir");
            Console.Write("\r\nSelecciona una opcion: ");
            switch (Console.ReadLine()) {
                case "1":
                    Console.Clear();
                    InstallMods();
                    return true;
                case "2":
                    Console.Clear();
                    InstallJar("forge");
                    return true;
                case "3":
                    Console.Clear();
                    InstallJar("optifine");
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }
        private static void InstallMods() {
            Console.WriteLine("===================================");
            Console.WriteLine("          Minecraft Tools          ");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine("¡Simplemente no hagas nada hasta que termine!");
            Console.WriteLine();
            var modsDir = MinecraftDir() + "/mods";
            if (!Directory.Exists(modsDir)) {
                Directory.CreateDirectory(modsDir);
            }
            Console.Write("Clear mods folder: ");
            DirectoryInfo di = new DirectoryInfo(modsDir);
            foreach (FileInfo file in di.GetFiles()) {
                file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.GetDirectories()) {
                dir.Delete(true); 
            }
            Console.WriteLine("Done!");
            Console.Write("Download mods.zip: ");
            var client = new WebClient ();
            client.DownloadFile(new Uri("https://asuna.tools/data/asunamc/mods.zip"), @modsDir + "/mods.zip");
            Console.WriteLine("Done!");
            Console.Write("Extract mods: ");
            System.IO.Compression.ZipFile.ExtractToDirectory(@modsDir + "/mods.zip", @modsDir);
            Console.WriteLine("Done!");
            Console.Write("Delete Temp: ");
            if(File.Exists(@modsDir + "/mods.zip")) {
                File.Delete(@modsDir + "/mods.zip");
            }
            Console.WriteLine("Done!");
            Thread.Sleep(300);
        }

        private static void InstallJar(String type) {
            switch (type) {
                case "forge": {
                    Console.Write("Download Forge: ");
                    var client = new WebClient();
                    client.DownloadFile(new Uri("https://asuna.tools/data/asunamc/forge.jar"), Path.GetTempPath() + "/forge.jar");
                    Console.WriteLine("Done!");
                    Console.WriteLine("Running Installer...");
                    Console.WriteLine();
                    try {
                        var psi = new ProcessStartInfo {
                            FileName = Globals.Java,
                            Arguments = " -jar " + Path.GetTempPath() + "/forge.jar",
                            RedirectStandardError = true,
                            UseShellExecute = false
                        };

                        var pr = Process.Start(psi);
                        pr?.WaitForExit();
                    }
                    catch (Exception) {
                        Console.WriteLine("Installer failed!");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Presione cualquier tecla para terminar la instalacion.");
                    Console.ReadLine();
                    break;
                }
                case "optifine": {
                    Console.Write("Download Optifine: ");
                    var client = new WebClient();
                    client.DownloadFile(new Uri("https://asuna.tools/data/asunamc/optifine.jar"), Path.GetTempPath() + "/optifine.jar");
                    Console.WriteLine("Done!");
                    Console.WriteLine("Running Installer...");
                    Console.WriteLine();
                    try {
                        var psi = new ProcessStartInfo {
                            FileName = Globals.Java,
                            Arguments = " -jar " + Path.GetTempPath() + "/optifine.jar",
                            RedirectStandardError = true,
                            UseShellExecute = false
                        };

                        var pr = Process.Start(psi);
                        pr?.WaitForExit();
                    }
                    catch (Exception) {
                        Console.WriteLine("Installer failed!");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Presione cualquier tecla para terminar la instalacion.");
                    Console.ReadLine();
                    break;
                }
            }
        }

        private static class Globals {
            public static String Java = "java";
        }

        private static bool CheckJava(string path) {
            try {
                var psi = new ProcessStartInfo {
                    FileName = path,
                    Arguments = " -version",
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                Process.Start(psi);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }
}
