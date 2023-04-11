
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public class Program
{
    private static void Main(string[] args)
    {
        AppBank appBank = new AppBank();

        string bahasa = Console.ReadLine();
        if(appBank.bankTransfer.lan == bahasa)
        {
            Console.WriteLine("Please insert the amount of money to transfer");
        } else
        {
            Console.WriteLine("Masukkan jumlah uang yang akan di-transfer");
        }
        string jmlTransfer = Console.ReadLine();
        int jml = Convert.ToInt32(jmlTransfer);
        int biaya = 0;
        if(jml<= appBank.bankTransfer.transfer.threshold)
        {
            biaya = appBank.bankTransfer.transfer.low_fee;
        } else
        {
            biaya = appBank.bankTransfer.transfer.high_fee;
        }
        int tot = jml + biaya;
        if (appBank.bankTransfer.lan == bahasa)
        {
            Console.WriteLine("Transfer fee = " + biaya + " Total amount = " + tot);
        } else
        {
            Console.WriteLine("Biaya transfer = " + biaya + " Total biaya = " + tot);
        }

        if(appBank.bankTransfer.lan == bahasa)
        {
            Console.WriteLine("Select transfer method : ");
        } else
        {
            Console.WriteLine("Pilih metode pembayaran : ");
        }
        string method = Console.ReadLine();
        for(int i = 0; i < appBank.bankTransfer.methods.Count; i++)
        {
            Console.WriteLine(i + ". " + appBank.bankTransfer.methods[i]);
        }
        if (appBank.bankTransfer.lan == bahasa)
        {
            Console.WriteLine("Please type" + appBank.bankTransfer.confirmation.en + " to confirm the transaction");
        } else
        {
            Console.WriteLine("Ketik " + appBank.bankTransfer.confirmation.id + " untuk mengonfrimasi transaksi");

        }
        string ya = Console.ReadLine();
        if(ya == appBank.bankTransfer.confirmation.en) {
            Console.WriteLine("The transfer is completed");
        } else if (ya == appBank.bankTransfer.confirmation.id)
        {
            Console.WriteLine("Proses transfer berhasil");
        } else
        {
            if (ya == appBank.bankTransfer.confirmation.en)
            {
                Console.WriteLine("Transfer is cancelled");
            } else if(ya == appBank.bankTransfer.confirmation.id)
            {
                Console.WriteLine("Transfer dicancel");
            }
       

    }
}

public class AppBank
{
    public BankTransferConfig bankTransfer;

    private const string fileLocation = "C:\\Users\\ACER\\praktikum_kpl\\jurnal_8\\modul8_1302213018\\modul8_1302213018\\bank_transfer_config.json";
    public AppBank()
    {
        try
        {
            ReadConfigFile();
        }
        catch
        {
            writeConfigFile();
        }
    }

    private BankTransferConfig ReadConfigFile()
    {
        string hasilBaca = File.ReadAllText(fileLocation);
        bankTransfer = JsonSerializer.Deserialize<BankTransferConfig>(hasilBaca);
        return bankTransfer;
    }

    private void writeConfigFile()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        string texttulis = JsonSerializer.Serialize(bankTransfer, options);
        File.WriteAllText(fileLocation, texttulis);
    }
}
public class BankTransferConfig
{
    public string lan { get; set; }
    public Transfer transfer { get; set; }
    public List<BankTransferConfig> methods { get; set; }
    public Confirmation confirmation { get; set; }

    public BankTransferConfig() { }
    public BankTransferConfig(string lan, Transfer transfer, List<BankTransferConfig> methods, Confirmation confirmation)
    {
        this.lan = lan;
        this.transfer = transfer;
        this.methods = methods;
        this.confirmation = confirmation;
    }

}

public class Transfer
{
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set;}

    public Transfer() { }
    public Transfer(int threshold, int low_fee, int high_fee)
    {
        this.threshold = threshold;
        this.low_fee = low_fee;
        this.high_fee = high_fee;
    }
}

public class Confirmation
{
    public string en { get; set; }
    public string id { get; set; }
    public Confirmation()
    {

    }

    public Confirmation(string en, string id)
    {
        this.en = en;
        this.id = id;
    }
}