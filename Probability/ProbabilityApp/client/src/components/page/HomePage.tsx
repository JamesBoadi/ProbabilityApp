
import { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { InputText } from 'primereact/inputtext';
import { Inputs, FunctionType } from '../../interface/interface';
import { Button } from "primereact/button";
import { fetchCalculateProbability } from '../../api/api';
import { isEmpty } from '../../helpers/helpers';

export const HomePage = () => {
    const [result, setResult] = useState();
    const [functionType, setFunctionType] = useState<FunctionType>({ functionType: '' });

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<Inputs>()

    const validateInput = (data: Inputs) => {
        const probA = data.probabilityA ?? -1;
        const probB = data.probabilityB ?? -1;

        if (probA === -1) {
            alert('probabilityA is empty')
            return false;
        }
        else if (probB === -1) {
            alert('probabilityB is empty')
            return false;
        }
        if (!(probA >= 0 && probA <= 1)) {
            alert('Enter a value for probabilityA between 0 and 1')
            return false;
        }
        else if (!(probB >= 0 && probB <= 1)) {
            alert('Enter a value for probabilityB between 0 and 1')
            return false;
        }

        if (isEmpty(functionType.functionType)) {
            alert('Select a function')
            return false;
        }

        return true;
    }

    const submitData: SubmitHandler<Inputs> = async (data: Inputs) => {
        const validate = validateInput(data);

        if (!validate)
            return;

        const res = await fetchCalculateProbability(data, functionType);
        setResult(res);
    }

    // Replace strings with enums (move to another file)
    return (
        <>
            <form onSubmit={handleSubmit(submitData)}>

                <h3>Probability App</h3>
                <div className="field">
                    <p>Probability A</p>
                    <InputText
                        type="number"
                        step="0.1"
                        id={"probabilityA"}
                        {...register("probabilityA")}
                    />

                    <p>{errors.probabilityA?.message}  </p>
                </div>
                <div className="field">
                    <p>Probability B</p>
                    <InputText
                        type="number"
                        step="0.1"
                        id={"probabilityB"}
                        {...register("probabilityB")}

                    />
                    <p>{errors.probabilityB?.message}</p>
                </div>

                <div className="button">
                    <Button
                        label="CombinedWith: P(A)P(B)"
                        id="CombinedWithFunction"
                        type="button" className="mt-2"
                        onClick={() => setFunctionType({ functionType: "CombinedWithFunction" })} />
                </div>
                <div className="button">
                    <Button
                        label="Either: P(A) + P(B) – P(A)P(B)"
                        id="EitherFunction"
                        type="button" className="mt-2"
                        onClick={() => setFunctionType({ functionType: "EitherFunction" })} />
                </div>
                <hr />
                <div className="button">
                    <Button type="submit" label="Calculate" className="mt-2" />
                </div>

                <p>Result: {result} </p>

            </form>
        </>)
}