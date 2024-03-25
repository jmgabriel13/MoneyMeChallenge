
export function monthsToYears(months: number): number {
    if (months < 0) {
        return 1 / 12;
    }

    return months / 12;
}
