# Versioning

We use Git tags to mark exact build snapshots so you can roll back to a known version.

## Tag format

Use semantic-ish versions like `v0.1.0`, `v0.2.0`, `v1.0.0`.

## Create a tag (manual)

```
git tag -a v0.2.0 -m "Build v0.2.0"
git push origin v0.2.0
```

## Roll back to a version

Checkout a tag (detached HEAD):

```
git checkout v0.2.0
```

Reset `main` to a tag (and push it):

```
git checkout main
git reset --hard v0.2.0
git push --force origin main
```

## Where to see versions

On GitHub, open the repo and click the "Tags" page to view all versions.
Locally, run `git tag` to list all tags.

## Scripted tagging

Use the helper script to create and push a tag:

```
./scripts/tag-build.ps1 -Version v0.2.0 -Message "Build v0.2.0"
```

Notes:
- Tags are immutable references to exact commits.
- If you need to update a tag, delete it locally and remotely, then recreate it.
