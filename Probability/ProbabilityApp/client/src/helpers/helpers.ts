export function isNullOrUndefined(value: any): boolean
{
    if(value === null || value === undefined)
        return true;

    return false;
}

export function isEmpty(value: string): boolean
{
    if(value === null || value === undefined || value === '')
        return true;

    return false;
}