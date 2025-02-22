namespace Kanboom.Models;

public class Domain {
    public class Group {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string GroupLink { get; set; }
        public long Fk_OwnerId { get; set; }
    }

    public class Board {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required int StagesCount { get; set; }
        public long? Fk_GroupId { get; set; }
        public required long Fk_BoardOwner { get; set; }
        public required bool IsGroupBoard { get; set; }
        public required string Invite { get; set; }
        public int? IncompletedTasks { get; set;}
        public List<long?> Users { get; set; }
        public List<Task>? Tasks { get; set; }
        public List<StageLevel>? StageLevels { get; set; }
    }

    public class Task {
        public long? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? StageNumber { get; set; }
        public long?  Fk_UserAssignee  { get; set; }
        public long? Fk_Board { get; set; }
        public bool Hidden { get; set; }
    }

    public class StageLevel {
        public required string StageName  { get; set; }
        public required int StageNumber { get; set; }
    }
}