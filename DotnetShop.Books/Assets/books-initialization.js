let books = {};
const getBooks = async () => {
    let response = fetch('https://localhost:7300/graphql/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            query: `
                query {
                    allBooksInfo {
                        id
                        title
                        language
                        thumbnailUrl
                        category {
                          id
                          name
                          code
                        }
                        authors {
                          id
                          firstName
                          lastName
                          salary
                        }
                      }
                }
            `,
        }),
    });

    return await response.then((response) => response.json()).then(result => result.data);
}

books = await getBooks();

books.allBooksInfo.forEach((item) => {
    let authors = [];
    item.authors.forEach((a) => {
        authors.push(`${a.firstName} ${a.lastName}`);
    })

    document.querySelector(".book-list").innerHTML += `
    <div class="col-lg-6 col-md-12">
            <div class="card mb-3">
                <div class="row g-0">
                    <div class="col-md-4">
                        <img src="https://localhost:7300${item.thumbnailUrl}" class="img-fluid rounded-start">
                    </div>
                    <div class="col-md-8">
                        <div class="card-body">
                            <h5 class="card-title">${item.title}</h5>
                            <div class="card-text">ID: ${item.id}</div>
                            <div class="card-text">Language: ${item.language}</div>
                            <p class="card-text">Authors: ${authors}</p>
                            <div class="card-text"><small class="text-muted">Coming soon</small></div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    `
});
