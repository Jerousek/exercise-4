namespace GameLibrary;

// TODO: Vytvořte třídu UpdatedStatsEventArgs, která je potomkem EventArgs
// - a obsahuje vlastnosti (get & init)
// -- int Correct
// -- int Missed
// -- int Accuracy

// TODO: Vytvořte delegát UpdatedStatsEventHandler pro příslušnou událost, využijte výše definované argumenty

// TODO: Dokončete třídu Stats...


public class UpdatedStatsEventArgs : EventArgs
{
    public UpdatedStatsEventArgs(int correct, int missed, int accuracy)
    {
        Correct = correct;
        Missed = missed;
        Accuracy = accuracy;
    }

    public int Correct { get; init; }
    public int Missed { get; init; }
    public int Accuracy { get; init; }

}

public delegate void UpdatedStatsEventHandler(object sender, UpdatedStatsEventArgs e);

public class Stats
{

    // TODO: Vytvořte vlastnosti určené pro čtení:
    // - int Correct
    // - int Missed
    // - Int Accuracy

    // TODO: Vytvořte veřejnou událost UpdatedStats (UpdatedStatsEventHandler?)

    // TODO: Vytvořte veřejnou metodu Update
    // - parametr - bool correctKey - určuje zdali byla stisknuta správná klávesa a jedná se o Correct zásah či o Missed zásah
    // - na základě parametru inkrementujte Correct nebo Missed vlastnost
    // - vypočtěte hodnotu Accuracy jako procentuální přesnost (na základě Correct a Missed, vypočtená hodnota 0-100 %)
    // - vyvolejte událost UpdatedStats a předejte pomocí event args aktuální stav vlastností

    // TODO: Vytvořte veřejnou metodu Reset
    // - metoda vynuluje vlasnosti Correct, Missed, Accuracy
    // - metoda nijak nemění stav události UpdatedStats a ani ji nevyvolává

    public int Correct { get; private set; }
    public int Missed { get; private set; }
    public int Accuracy { get; private set; }

    public event UpdatedStatsEventHandler? UpdatedStats;

    protected virtual void onUpdatedStats(UpdatedStatsEventArgs eventArgs)
    {
        UpdatedStats?.Invoke(this, eventArgs);
    }

    public void Update(bool correctKey) 
    {
        if (correctKey)
        {
            Correct++;
        } else
        {
            Missed++;
        }
        double pomer = Math.Round((double)Correct * 100 / (Missed + Correct));
        Accuracy = (int)(pomer);
        onUpdatedStats(new UpdatedStatsEventArgs(this.Correct, this.Missed, this.Accuracy));
      
    }

    public void Reset()
    {
        Correct = 0;
        Missed = 0;
        Accuracy = 0;
    }
}
