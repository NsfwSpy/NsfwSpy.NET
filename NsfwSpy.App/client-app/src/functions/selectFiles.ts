/**
 * Custom type for file input element (`<input type="file" />`).
 */
type InputFile = HTMLInputElement & {
    capture?: boolean | string;
};

/**
 * Type of options for file input element (`<input type="file" />`) virtually
 * created to select files.
 */
export type Options = {
    /**
     * Defines accepted file types. It's a comma-separated list of file
     * extensions, mime-types or unique file type specifiers.
     *
     * https://developer.mozilla.org/docs/Web/HTML/Element/input/file#Unique_file_type_specifiers
     *
     * @example ```js
     * "image/*,video/*,.pdf,.doc,.docx,.xls"
     * ```
     */
    accept?: string;

    /**
     * Combined with `accept` property it specifies which camera to use for
     * capture of image or video. It was previously a Boolean value.
     */
    capture?: string | null;

    /**
     * Allow multiple files selection.
     */
    multiple?: boolean;
};

/**
 * Creates a virtual file input element (`<input type="file" />`) with options.
 * @param options
 */
const createInputFile = ({
    accept = '',
    capture = null,
    multiple = false,
}: Options = {}): InputFile => {
    const input = document.createElement('input') as InputFile;

    input.type = 'file';
    input.accept = accept;
    if (capture !== null)
        input.capture = capture;
    input.multiple = multiple;

    return input;
};

/**
 * Virtually creates a file input element (`<input type="file" />`), triggers it
 * and returns selected files.
 *
 * @example
 * selectFiles({ accept: 'image/*', multiple: true }).then(files => {
 *   // ...
 * });
 *
 * @param options
 */
export const selectFiles = (options?: Options) =>
    new Promise<null | FileList>((resolve) => {
        const input = createInputFile(options);

        input.addEventListener('change', () => resolve(input.files || null));

        setTimeout(() => {
            const event = new MouseEvent('click');
            input.dispatchEvent(event);
        }, 0);
    });
