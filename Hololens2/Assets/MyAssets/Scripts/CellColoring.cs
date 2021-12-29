using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.Utilities;

public class CellColoring : MonoBehaviour
{
    public static GameObject arrowWaypointer;


    void Start()
    {

        arrowWaypointer = GameObject.Find("ChevronArrow");
        arrowWaypointer.SetActive(false);

    }

    /**
    * Colors cells of faulted processes red.
    * @param     cells the cells of the production line.
    * @return    void.
    * 
    */
    public static void ColorCellsRed(List<Cell> cells)
    {

        Material FaultMaterial = Resources.Load("mat_5.001", typeof(Material)) as Material;

        foreach (Cell cell in cells) { 

            cell.getCellObject().GetComponent<MeshRenderer>().material = FaultMaterial;

        }

    }

    /**
    * Points the waypointer to the given cell.
    * @param     cell the cell the waypointer should point to.
    * @return    void.
    * 
    */
    public static void SetArrowWaypointer(Cell cell) {

        arrowWaypointer.SetActive(true);
        arrowWaypointer.GetComponent<DirectionalIndicator>().DirectionalTarget = cell.getCellObject().transform;

    }

    /**
    * Disables the waypointer arrow from the scene.
    * @param     none.
    * @return    void.
    * 
    */
    public static void DisableArrowWaypointer()
    {
        arrowWaypointer.SetActive(false);
    }

    /**
    * Finds a list of cells that corespond to the given processes
    * @param     processes the list of processes.
    * @param     cells the list of available cells.
    * @return    void.
    * 
    */
    public static List<Cell> getCell(List<string> processes, List<Cell> cells) {
        List<Cell> foundcells = new List<Cell>();

        foreach (Cell cell in cells)
        {

            foreach (string process in processes)
            {


                if (cell.getCellProcesses().Contains(process))
                {

                    foundcells.Add(cell);

                }

            }

        }

        return foundcells;

    }


    /**
    * Colors all cells black.
    * @param     cells the list of cells of the productionline
    * @return    void.
    */
    public static void ColorCellsBlack(List<Cell> cells)
    {

        Material DefaultMaterial = Resources.Load("vuvg_grouped_final", typeof(Material)) as Material;

        foreach (Cell cell in cells)
        {

            cell.getCellObject().GetComponent<MeshRenderer>().material = DefaultMaterial;

        }

    }


    /**
   * Hardcoded positions of the production line.
   * TODO: For some reason the positions (transforms) of the gameObjects of the production line are all the same.
   * This was a workaround.
   * @param     processes the list of processes.
   * @param     cells the list of available cells.
   * @return    void.
   */
    public static Vector3 GetCellPositionByName(string cellname)
    {

        if (cellname == "cell_1" || cellname == "cell_1_b")
        {
            return new Vector3(-10.5f, 1.8f, 1.8f);
        }
        else if (cellname == "cell_2" || cellname == "cell_2_b")
        {
            return new Vector3(-7.9f, 1.8f, 1.8f);
        }
        else if (cellname == "cell_3" || cellname == "cell_3_b")
        {
            return new Vector3(-5f, 1.8f, 1.8f);
        }
        else if (cellname == "cell_4" || cellname == "cell_4_b")
        {
            return new Vector3(-3.2f, 1.8f, 1.8f);
        }
        else if (cellname == "cell_5" || cellname == "cell_5_b")
        {
            return new Vector3(-0.8f, 1.8f, 1.8f);
        }
        else if (cellname == "cell_6" || cellname == "cell_6_b")
        {
            return new Vector3(2.2f, 1.8f, 1.8f);
        }
        else if (cellname == "cell_7" || cellname == "cell_7_b")
        {
            return new Vector3(5.4f, 1.8f, 1.8f);
        }
        else if (cellname == "cell_8" || cellname == "cell_8_b")
        {
            return new Vector3(8.2f, 1.8f, 1.8f);
        }
        else { return new Vector3(-10.5f, 1.8f, 1.8f); }

    }

}
