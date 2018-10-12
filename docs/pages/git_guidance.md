## Git Guidance

### Configuring the Upstream Repository

_upstream = the original repository at https://github.com/Magenic/Badge-Application_

1. Open cmd or PowerShell and change directories to the location of the fork you cloned, i.e. `cd 'D:\Dev\Badge-App\nikkibarros\Badge-Application'`

2. Type `git remote -v` and press _Enter_. You'll see the current configured remote repository for your fork (only origin by default).

   ![git remote dash v 1][gitRemoteV1]

3. Type `git remote add upstream https://github.com/Magenic/Badge-Application`, and then press _Enter_. It will look like this:

   ![git add upstream][gitAddUpstream]

4. To verify the new upstream repository you've specified for your fork, type `git remote -v` again. You should see the URL for your fork as _origin_, and the URL for the original repository as _upstream_.

   ![git remote dash v 2][gitRemoteV2]

From: https://help.github.com/articles/fork-a-repo/

### Syncing the Repositories

_Please ensure you sync the fork with the upstream, and merge_ master _to your feature branch before pushing any changes to avoid breaking the codebase and conflicts in the future._

1. Open cmd or PowerShell and change directories to the location of the fork you cloned, i.e. `cd 'D:\Dev\Badge-App\nikkibarros\Badge-Application'`

2. Fetch the branches and their respective commits from the upstream repository. Commits to _master_ will be stored in a local branch, _upstream/master_.

   ![git fetch upstream][gitFetchUpstream]

3. Check out your fork's local _master_ branch.

   ![git checkout][gitCheckout]

4. Merge the changes from _upstream/master_ into your local _master_ branch. This brings your fork's _master_ branch into sync with the upstream repository, without losing your local changes. At the time of writing, no updates have been made to the upstream since the fork was created.

   ![git merge][gitMerge]

5. If there are changes that came from the upstream, do a normal merge from the local _master_ branch of your fork to your feature branch.

From: https://help.github.com/articles/syncing-a-fork/

### Git Process

1. Make use of feature/hotfix branches

2. Sync _upstream/master_ to _origin/master_

3. Merge _origin/master_ to _origin/<feature/hotfix>_

4. Rebuild the solution

5. Commit, push

6. Submit a PR to have your feature/hotfix branch merged back to _origin/master_

7. Submit another PR to have _origin/master_ merged back to _upstream/master_



[gitRemoteV1]: ./../files/git_remote_v_1.png
[gitAddUpstream]: ./../files/git_add_upstream.png
[gitRemoteV2]: ./../files/git_remote_v_2.png
[gitFetchUpstream]: ./../files/git_fetch_upstream.png
[gitCheckout]: ./../files/git_checkout.png
[gitMerge]: ./../files/git_merge.png