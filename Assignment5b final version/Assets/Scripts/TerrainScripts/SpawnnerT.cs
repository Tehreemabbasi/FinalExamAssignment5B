using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;

public class SpawnnerT : MonoBehaviour
{
    public static int count = 0;
    public GameObject cube;
    public static System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        int c = 0;
        for (int i = 0; i < 58;)
        {

            //create random positions and pass this position variable to Instantiate function to create cube at these locations
            Vector3 position = new Vector3(UnityEngine.Random.Range(2, 38), UnityEngine.Random.Range(0.5f, 0.5f), UnityEngine.Random.Range(2, 32));

            float x = 0;
            if (Physics.CheckSphere(position, x))
            {

            }
            else
            {
                //generate randome cubes at random positions with random string
                GameObject newCube = Instantiate(cube, position, Quaternion.identity);
                cube.transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
                string cubeText = RandomString(random.Next(9, 15));
                //convert random string into balanced
                System.Random rn = new System.Random();
                if (c <= 18)
                {
                    string s = cubeText.Substring(0, cubeText.Length -5);
                    s = balancedBrackets(s);
                    cube.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text = s;
                    c++;
                }
                else
                    cube.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text = cubeText;
                i++;

                char[] exp= new char[cubeText.Length];
                //check for balanced parenthesis
                string cubeFinalValue = cube.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text;
                for (int k = 0; k < cubeFinalValue.Length; k++)
                {
                    exp[k] = cubeFinalValue[k];
                }
                if (areParenthesisBalanced(exp))
                {
                    newCube.tag = "parenthesis";
                    count++;
                }
          
            }
        }

    }
    //func that generate random strings
    public static string RandomString(int length)
    {
        const string chars = "(xt8)";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    //this function is called in playerController class


    public static string balancedBrackets(string str)
    {
        // Initializing dep to 0 
        int dep = 0;

        // Stores maximum negative depth 
        int minDep = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '(')
                dep++;
            else if (str[i] == ')')
                dep--;

            // if dep is less than minDep 
            if (minDep > dep)
                minDep = dep;
        }

        // if minDep is less than 0 then there 
        // is need to add '(' at the front 
        if (minDep < 0)
        {
            for (int i = 0; i < Math.Abs(minDep); i++)
                str = '(' + str;
        }

        // Reinitializing to check the updated string 
        dep = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '(')
                dep++;
            else if (str[i] == ')')
                dep--;
        }

        // if dep is not 0 then there 
        // is need to add ')' at the back 
        if (dep != 0)
        {
            for (int i = 0; i < dep; i++)
                str = str + ')';
        }
        return str;
    }


    static Boolean isMatchingPair(char character1, char character2)
    {
        if (character1 == '(' && character2 == ')')
            return true;
        else
            return false;
    }

    /* Return true if expression has balanced  
    Parenthesis */
    static Boolean areParenthesisBalanced(char[] exp)
    {
        /* Declare an empty character stack */
        Stack<char> st = new Stack<char>();

        /* Traverse the given expression to  
            check matching parenthesis */
        for (int i = 0; i < exp.Length; i++)
        {

            /*If the exp[i] is a starting  
                parenthesis then push it*/
            if (exp[i] ==  '(')
                st.Push(exp[i]);

            /* If exp[i] is an ending parenthesis  
                then pop from stack and check if the  
                popped parenthesis is a matching pair*/
            if (exp[i] == ')')
            {

                /* If we see an ending parenthesis without  
                    a pair then return false*/
                if (st.Count == 0)
                {
                    return false;
                }

                /* Pop the top element from stack, if  
                    it is not a pair parenthesis of character  
                    then there is a mismatch. This happens for  
                    expressions like {(}) */
                else if (!isMatchingPair(st.Pop(), exp[i]))
                {
                    return false;
                }
            }
        }

        /* If there is something left in expression  
            then there is a starting parenthesis without  
            a closing parenthesis */

        if (st.Count == 0)
            return true; /*balanced*/
        else
        { /*not balanced*/
            return false;
        }
    }

}

