using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<Cell> cells;
    

    void Start()
    {
        cells = FillCells();
        GameObject diagnostics = GameObject.Find("Diagnostics");
        diagnostics.SetActive(false);
    }


    /**
  * Creates the mapping between Cells and Processes
  * @param     none.
  * @return    Cells the list of available cells.
  */
    private static List<Cell> FillCells() {
        List<Cell> cells = new List<Cell>();

        Cell Cell1 = new Cell(new List<string> {"P01", "P02" , "P03"} ,"cell_1", GameObject.Find("cell_1"));
        cells.Add(Cell1);

        Cell Cell2 = new Cell(new List<string> {"P04", "P05", "P06", "P08"},"cell_2", GameObject.Find("cell_2"));
        cells.Add(Cell2);

        Cell Cell3 = new Cell(new List<string> {"P09"},"cell_3", GameObject.Find("cell_3"));
        cells.Add(Cell3);

        Cell Cell4 = new Cell(new List<string> {"P11"},"cell_4", GameObject.Find("cell_4"));
        cells.Add(Cell4);

        Cell Cell5 = new Cell(new List<string> {"P12", "P13"},"cell_5", GameObject.Find("cell_5"));
        cells.Add(Cell5);

        Cell Cell6 = new Cell(new List<string> { "P14", "P15", "P16"},"cell_6", GameObject.Find("cell_6"));
        cells.Add(Cell6);

        Cell Cell7 = new Cell(new List<string> {"P18", "P20"},"cell_7", GameObject.Find("cell_7"));
        cells.Add(Cell7);

        return cells;
    }

    

}
