function Download(filename, data, contentType) {
    const file = new File([data], filename, { type: contentType });
    const exportUrl = URL.createObjectURL(file);

    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = filename;
    a.target = "_self";
    a.click();

    URL.revokeObjectURL(exportUrl);
    a.remove();
}

function Upload(allowed) {
    return new Promise((resolve, reject) => {
        const input = document.createElement("input");
        input.type = "file";
        input.accept = allowed;

        input.onchange = async () => {
            if (input.files.length > 0) {
                const file = input.files[0];

                if (file.type !== allowed) {
                    reject("Pogrešan tip datoteke.");
                }

                const reader = new FileReader();

                reader.onload = () => {
                    resolve(reader.result);
                };

                reader.onerror = (error) => {
                    reject(`Greška: ${error}`);
                };

                reader.readAsText(file);
            } else {
                reject("Nije odabrana niti jedna datoteka.");
            }
        };

        input.click();
    });
}