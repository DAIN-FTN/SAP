function isToday(date: Date): boolean {
    const today = new Date();

    return date.getDate() === today.getDate() &&
        date.getMonth() === today.getMonth() &&
        date.getFullYear() === today.getFullYear();
}

function isTomorrow(date: Date): boolean {
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);

    return date.getDate() === tomorrow.getDate() &&
        date.getMonth() === tomorrow.getMonth() &&
        date.getFullYear() === tomorrow.getFullYear();
}

function formatDate(date: Date): string {
    const day = date.getDate().toString();
    const month = (date.getMonth() + 1).toString();
    const year = date.getFullYear().toString();

    return `${day}.${month}.${year}`;
}

function getTimeAsString(date: Date): string {
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');

    return `${hours}:${minutes}`;
}

function getMeaningfulDate(date: Date) {
    if (!date) {
        return '/';
    }

    if (isToday(date)) {
        return `Today, ${getTimeAsString(date)}`;
    }

    if (isTomorrow(date)) {
        return `Tomorrow, ${getTimeAsString(date)}`;
    }

    return `${formatDate(date)}, ${getTimeAsString(date)}`;
}

export const DateUtils = {
    isToday,
    isTomorrow,
    formatDate,
    getTimeAsString,
    getMeaningfulDate
};