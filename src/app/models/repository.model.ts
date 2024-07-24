export type Repository = {
    total_count: number,
    items: RepositoryItem[]
}

export type RepositoryItem = {
    id: number,
    name: string,
    description: string,
    owner: {
      avatar_url: string
    },
    avatar: string,
    url: string,
    isMarked: boolean
}
