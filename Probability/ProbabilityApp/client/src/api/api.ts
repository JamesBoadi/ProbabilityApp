import { Inputs, FunctionType } from '../interface/interface';

export async function fetchCalculateProbability(inputs: Inputs, functionType: FunctionType): Promise<any> {
    try {
        let response = await fetch(`http://localhost:5289/calculateProbability`, {
            method: "POST",
            body: JSON.stringify(inputs),
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
                "Function-Type": functionType.functionType
            }
        })
       
        if (!response.ok) {
            console.log('error! status-code: ' + response.status + ' message: ' + response.statusText);
            alert('Bad request');
            return;
        }

        return response.text();
    }
    catch (ex: any) {
        console.log('error ' + ex);
        alert('There was a problem with the submission, check the server');
    }
}


