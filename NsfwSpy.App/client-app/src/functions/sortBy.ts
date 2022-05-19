import { NsfwSpyResult } from '../models/NsfwSpyResult';

export const sortNsfwResult = (result: NsfwSpyResult) => {
    const validKeys = ['hentai', 'neutral', 'pornography', 'sexy'];
    const sortableArray = Object.entries(result);
    let sortedArray = sortableArray.sort(([, a], [, b]) => b - a);
    sortedArray = sortableArray.filter((i) => validKeys.includes(i[0]));
    return sortedArray;
}